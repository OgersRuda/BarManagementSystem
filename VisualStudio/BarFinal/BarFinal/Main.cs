using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;

namespace BarFinal
{
    public partial class Main : Form
    {
        String stdDetails = "{0, -52}{1, -17}{2, -10}";

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ogers-Ruda\Documents\Bar.mdf;Integrated Security=True;Connect Timeout=30");
        Dictionary<string, int> dict = new Dictionary<string, int>();
        int Total = 0;
        int index;
        int prodPrice;

        string formattedTime = DateTime.Now.ToString("dd.MM.yyyy  hh:mm");


        public Main()
        {
             
            InitializeComponent();
            button14.Enabled = false;

            Data.Text = formattedTime;
            hapTavolinat();

        }

        public void hapTavolinat()
        {
            flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select description from Tavolina";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from Tavolina";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 82;
                l.Height = 75;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(1);
                PaneliTavolinave.Controls.Add(l);


                l.Click += new System.EventHandler(TableClick);


            }
        }


        void TableClick(object Sender, EventArgs e)
        {

            Button currentButton = (Button)Sender;
            
            connection.Open();
            SqlDataReader dataReader;
            //SqlDataReader dataReader1;
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            //SqlCommand cmd2 = connection.CreateCommand();

            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            //cmd2.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Tavolina";
            //cmd2.CommandText = "select TblProduct.Description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProduct.Description='"+currentButton.Text+"'";

            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";

            connection.Close();

            tavolineNr.Text = currentButton.Text;



        }




        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
             DataTable dta = new DataTable();
             SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
             dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
             DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql,connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i=0; i<cnt; i++)
            {
                Button l = new Button();
                l.Name ="lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                     flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);
        
                    
            }
  


        }
        

        void buttonClick(object Sender, EventArgs e)
        { 
           
            Button currentButton = (Button)Sender;
            connection.Open();
            SqlDataReader dataReader;
            SqlDataReader dataReader1;
            SqlCommand cmd = connection.CreateCommand();
            SqlCommand cmd1 = connection.CreateCommand();
            //SqlCommand cmd2 = connection.CreateCommand();

            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd1.CommandType = CommandType.Text;
            //cmd2.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProduct.Description='"+currentButton.Text+"'";
            //cmd2.CommandText = "select TblProduct.Description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProduct.Description='"+currentButton.Text+"'";

            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();

            int [] array = new int[500];
            dataReader = cmd.ExecuteReader();
            if (tavolineNr.Text.Equals(""))
            {
                Total = 0;
                Totali.Text = "";
                listBox1.Items.Clear();
            }
            else { 
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    bool flag = false;
                    
                    if (!dict.ContainsKey(dataReader["description"].ToString()))
                    { dict.Add(dataReader["description"].ToString(), 1);
                        listBox1.Items.Add(String.Format(stdDetails,dataReader["description"].ToString()+".",dict[dataReader["description"].ToString()],(dataReader["price"]).ToString()));

                    }
                    else
                    {
                        dict[dataReader["description"].ToString()] += 1;
                        for(int i=listBox1.Items.Count-1; i>=0; i--)
                        {
                            
                            if (listBox1.Items[i].ToString().ToLower().Contains(dataReader["description"].ToString().ToLower()) && listBox1.Items[i].ToString().Split('.')[0].Length==dataReader["description"].ToString().Length)
                            listBox1.SetSelected(i, true);
                            index = listBox1.SelectedIndex;
                            listBox1.GetSelected(i);
                            flag = true;
                        }

                        if (flag)
                        {
                            //listBox1.Items.Add(String.Format(stdDetails, dataReader["description"].ToString(), dict[dataReader["description"].ToString()], dataReader["price"].ToString()));
                            listBox1.Items.RemoveAt(index);
                            int prodPrice = Convert.ToInt32(dataReader["price"])* dict[dataReader["description"].ToString()];
                            listBox1.Items.Insert(index, String.Format(stdDetails, dataReader["description"].ToString()+".", dict[dataReader["description"].ToString()], prodPrice.ToString()));

                        }

                    }

                    //label16.Text = dict[dataReader["description"].ToString()].ToString();
                    String item = dataReader["description"].ToString()+"                "+dataReader["Price"].ToString();

                    //listBox1.Items.Add(dataReader["description"].ToString());

                    Total +=Convert.ToInt32(dataReader["Price"]);
             
                    Totali.Text = Total.ToString()+" ALL";
                }


            }
            }



            connection.Close();


        }

        
        private void label13_Click_1(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        Button addButton(int i)
        {
            Button l = new Button();
            l.Name = "lb" + i.ToString();
            l.Text = "lb" + i.ToString();
            l.Font = new Font("Serif", 20, FontStyle.Bold);
            l.BackColor = Color.Gray;
            l.Width = 212;
            l.Height = 54;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Margin = new Padding(0);

            return l;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void button57_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=2";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=2";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
        

        }

        private void butoniBirra_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=3";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=3";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
          
        }

        private void butoniAlkol_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=4";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=4";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
           
        }

        private void butoniVendi_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=5";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=5";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
        
        }

        private void butoniVeraHapur_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=6";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=6";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
           
        }

        private void butoniVera_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=7";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=7";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
        
        }

        private void butoniAkullore_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=8";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=8";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
           
        }

        private void butoniEmbelsira_Click(object sender, EventArgs e)
        {
                flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=9";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=9";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 212;
                l.Height = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                    flowLayoutPanel1.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
         
        }


        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            int occurrence;
            ListBox.SelectedObjectCollection selected = new ListBox.SelectedObjectCollection(listBox1);
            selected = listBox1.SelectedItems;
            if(listBox1.SelectedIndex !=-1)
            {

                string de = listBox1.SelectedItem.ToString().Split('.')[0];
                //de = Reverse(de);
                //de = de.Split('e')[0];
                //de = Reverse(de);
                occurrence = dict[de];
                for (int i = selected.Count - 1; i >= 0; i--)
                { listBox1.Items.Remove(selected[i]);
                }
                    dict.Remove(de);

                string descr = listBox1.GetItemText(listBox1.SelectedItems);


                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                //SqlCommand cmd1 = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd1.CommandType = CommandType.Text;
                cmd.CommandText = "select TblProduct.price from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProduct.Description='"+de+"'";
                //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
                cmd.ExecuteNonQuery();
                //cmd1.ExecuteNonQuery();
                DataTable dta = new DataTable();
                SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
                dataadp.Fill(dta);
                string price = dta.Rows[0][0].ToString();
                connection.Close();
                label16.Text = de;
                Total -= Convert.ToInt32(price)*occurrence;
                Totali.Text = Total.ToString()+" ALL";

            }

        }



        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ListProdukt_Click(object sender, EventArgs e)
        {
            Produktet products = new Produktet();
            products.Show();
            //this.Hide();
        }



        void btnClick(object Sender, EventArgs e)
        {

            Button currentButton = (Button)Sender;
            

        }

        private void Sell_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select TblProduct.description from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=9";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=9";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            //SqlCommand cmd1 = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd1.CommandType = CommandType.Text;
            cmd.CommandText = "select description from Tavolina";
            //cmd1.CommandText = "select count(*) from TblProduct,TblProductType where TblProduct.ProductType=TblProductType.ProductType and TblProductType.ProductType=1";
            cmd.ExecuteNonQuery();
            //cmd1.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);

            //connection.Close();
            string sql = "select * from Tavolina";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            string count = ds.Tables[0].Rows.Count.ToString();
            int cnt = Convert.ToInt32(count);
            connection.Close();
            for (int i = 0; i < cnt; i++)
            {
                Button l = new Button();
                l.Name = "lb" + dta.Rows[i][0].ToString();
                l.Text = dta.Rows[i][0].ToString();
                l.Font = new Font("Serif", 13, FontStyle.Bold);
                l.ForeColor = Color.Black;
                l.BackColor = Color.LightGray;
                l.Width = 83;
                l.Height = 75;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Margin = new Padding(0);
                PaneliTavolinave.Controls.Add(l);

                l.Click += new System.EventHandler(buttonClick);


            }
        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void PaneliTavolinave_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
