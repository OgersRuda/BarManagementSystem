using System;
#if EF6
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
#endif
using System.Threading;
using System.Threading.Tasks;
#if EFCore
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
#elif EF6
#endif

namespace EntityFramework.DbContextScope.Interfaces
{
#if EFCore
    public interface IDbContext : IDisposable
#elif EF6
    public interface IDbContext : IDisposable, IObjectContextAdapter
#endif
    {
#if EFCore
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
#elif EF6
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
#endif

        int SaveChanges();
#if !NET40
        Task<int> SaveChangesAsync(CancellationToken cancelToken);
#endif
    }
}