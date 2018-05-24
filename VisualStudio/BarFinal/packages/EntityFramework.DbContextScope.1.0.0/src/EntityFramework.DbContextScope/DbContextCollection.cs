﻿/* 
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

#if !NET40
using System.Runtime.ExceptionServices;
#endif
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
#if EF6
using System.Data.Entity;
#endif
using EntityFramework.DbContextScope.Interfaces;
#if EFCore
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
#elif EF6

#endif

namespace EntityFramework.DbContextScope
{
    /// <summary>
    /// As its name suggests, DbContextCollection maintains a collection of DbContext instances.
    /// 
    /// What it does in a nutshell:
    /// - Lazily instantiates DbContext instances when its Get Of TDbContext () method is called
    /// (and optionally starts an explicit database transaction).
    /// - Keeps track of the DbContext instances it created so that it can return the existing
    /// instance when asked for a DbContext of a specific type.
    /// - Takes care of committing / rolling back changes and transactions on all the DbContext
    /// instances it created when its Commit() or Rollback() method is called.
    /// 
    /// </summary>
    public class DbContextCollection : IDbContextCollection
    {
        private readonly Dictionary<Type, IDbContext> _initializedDbContexts;
#if EFCore
        private readonly Dictionary<IDbContext, IDbContextTransaction> _transactions; 
#elif EF6
        private readonly Dictionary<IDbContext, DbContextTransaction> _transactions;
#endif
        private IsolationLevel? _isolationLevel;
        private readonly IDbContextFactory _dbContextFactory;
        private bool _disposed;
        private bool _completed;
        private readonly bool _readOnly;

        internal Dictionary<Type, IDbContext> InitializedDbContexts => _initializedDbContexts;

        public DbContextCollection(bool readOnly = false, IsolationLevel? isolationLevel = null, IDbContextFactory dbContextFactory = null)
        {
            _disposed = false;
            _completed = false;

            _initializedDbContexts = new Dictionary<Type, IDbContext>();
#if EFCore
            _transactions = new Dictionary<IDbContext, IDbContextTransaction>();
#elif EF6
            _transactions = new Dictionary<IDbContext, DbContextTransaction>();
#endif


            _readOnly = readOnly;
            _isolationLevel = isolationLevel;
            _dbContextFactory = dbContextFactory;
        }

        public TDbContext Get<TDbContext>() where TDbContext : class, IDbContext
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");

            Type requestedType = typeof(TDbContext);

            if (!_initializedDbContexts.ContainsKey(requestedType))
            {
                // First time we've been asked for this particular DbContext type.
                // Create one, cache it and start its database transaction if needed.
                TDbContext dbContext = _dbContextFactory != null
                    ? _dbContextFactory.CreateDbContext<TDbContext>()
                    : Activator.CreateInstance<TDbContext>();

                _initializedDbContexts.Add(requestedType, dbContext);

#if EFCore
                dbContext.ChangeTracker.AutoDetectChangesEnabled = !_readOnly;
#elif EF6
                dbContext.Configuration.AutoDetectChangesEnabled = !_readOnly;
#endif

                if (_isolationLevel.HasValue)
                {
#if EFCore
                    IDbContextTransaction tran = dbContext.Database.BeginTransaction(_isolationLevel.Value);
#elif EF6
                    DbContextTransaction tran = dbContext.Database.BeginTransaction(_isolationLevel.Value);
#endif
                    _transactions.Add(dbContext, tran);
                }
            }

            return _initializedDbContexts[requestedType] as TDbContext;
        }

        public int Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            // Best effort. You'll note that we're not actually implementing an atomic commit 
            // here. It entirely possible that one DbContext instance will be committed successfully
            // and another will fail. Implementing an atomic commit would require us to wrap
            // all of this in a TransactionScope. The problem with TransactionScope is that 
            // the database transaction it creates may be automatically promoted to a 
            // distributed transaction if our DbContext instances happen to be using different 
            // databases. And that would require the DTC service (Distributed Transaction Coordinator)
            // to be enabled on all of our live and dev servers as well as on all of our dev workstations.
            // Otherwise the whole thing would blow up at runtime. 

            // In practice, if our services are implemented following a reasonably DDD approach,
            // a business transaction (i.e. a service method) should only modify entities in a single
            // DbContext. So we should never find ourselves in a situation where two DbContext instances
            // contain uncommitted changes here. We should therefore never be in a situation where the below
            // would result in a partial commit. 

#if !NET40
            ExceptionDispatchInfo lastError = null;
#else
            Exception lastError = null;
#endif
            int c = 0;

            foreach (IDbContext dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    if (!_readOnly)
                    {
                        c += dbContext.SaveChanges();
                    }

                    // If we've started an explicit database transaction, time to commit it now.
#if EFCore
                    IDbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#elif EF6
                    DbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#endif
                    if (tran != null)
                    {
                        tran.Commit();
                        tran.Dispose();
                    }
                }
                catch (Exception e)
                {
#if !NET40
                    lastError = ExceptionDispatchInfo.Capture(e);
#else
                    lastError = e;
#endif
                }
            }

            _transactions.Clear();
            _completed = true;

#if !NET40
            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track
#else
            if (lastError != null)
            {
                throw new Exception("Error occurred committing transaction.", lastError);
            }
#endif
            return c;
        }

#if !NET40
        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null)
                throw new ArgumentNullException(nameof(cancelToken));
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            // See comments in the sync version of this method for more details.

            ExceptionDispatchInfo lastError = null;

            int c = 0;

            foreach (IDbContext dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    if (!_readOnly)
                    {
                        c += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);
                    }

                    // If we've started an explicit database transaction, time to commit it now.
#if EFCore
                    IDbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#elif EF6
                    DbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#endif
                    if (tran != null)
                    {
                        tran.Commit();
                        tran.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            _transactions.Clear();
            _completed = true;

            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track

            return c;
        }
#endif
        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

#if !NET40
            ExceptionDispatchInfo lastError = null;
#else
            Exception lastError = null;
#endif

            foreach (IDbContext dbContext in _initializedDbContexts.Values)
            {
                // There's no need to explicitly rollback changes in a DbContext as
                // DbContext doesn't save any changes until its SaveChanges() method is called.
                // So "rolling back" for a DbContext simply means not calling its SaveChanges()
                // method. 

                // But if we've started an explicit database transaction, then we must roll it back.
#if EFCore
                    IDbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#elif EF6
                DbContextTransaction tran = GetValueOrDefault(_transactions, dbContext);
#endif
                if (tran != null)
                {
                    try
                    {
                        tran.Rollback();
                        tran.Dispose();
                    }
                    catch (Exception e)
                    {
#if !NET40
                        lastError = ExceptionDispatchInfo.Capture(e);
#else
                        lastError = e;
#endif
                    }
                }
            }

            _transactions.Clear();
            _completed = true;

#if !NET40
            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track
#else
            if (lastError != null)
            {
                throw new Exception("Error occurred committing transaction.", lastError);
            }
#endif      
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            // Do our best here to dispose as much as we can even if we get errors along the way.
            // Now is not the time to throw. Correctly implemented applications will have called
            // either Commit() or Rollback() first and would have got the error there.

            if (!_completed)
            {
                try
                {
                    if (_readOnly) Commit();
                    else Rollback();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            foreach (IDbContext dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            _initializedDbContexts.Clear();
            _disposed = true;
        }

        /// <summary>
        /// Returns the value associated with the specified key or the default 
        /// value for the TValue  type.
        /// </summary>
        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}