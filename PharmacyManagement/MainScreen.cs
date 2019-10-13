using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PharmacyManagement
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var l = new Login();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void tabPage10_Enter(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Name FROM pharmacy", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        dataGridView1.DataSource = dt;
                        DataGridViewColumn column = dataGridView1.Columns[0];
                        column.Width = 150;
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage2_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Name FROM pharmacy", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        dataGridView2.DataSource = dt;
                        DataGridViewColumn column = dataGridView2.Columns[0];
                        column.Width = 150;
                        dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage5_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM medicine", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        dataGridView3.DataSource = dt;
                        DataGridViewColumn column = dataGridView3.Columns[0];
                        column.Width = 150;
                        dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"insert into medicine (Name,Stock,Report,Usage) values(@Name,@Stock,@Report,@Usage) ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", name.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", stock.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Report", report.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Usage", usage.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            name.Clear();
            stock.Clear();
            report.Clear();
            usage.Clear();
            approval.Text = "Added Successfully";
        }

        private void tabPage7_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM patient", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        dataGridView4.DataSource = dt;
                        DataGridViewColumn column = dataGridView4.Columns[0];
                        column.Width = 120;
                        dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"insert into patient (ID,Name,Surname,Age,City,HaveReport) values(@ID,@Name,@Surname,@Age,@City,@HaveReport) ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@ID", ID.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Name", pat_name.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Surname", pat_surname.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Age", pat_age.Text));
            cmd.Parameters.Add(new SQLiteParameter("@City", pat_city.Text));
            cmd.Parameters.Add(new SQLiteParameter("@HaveReport", pat_report.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            ID.Clear();
            pat_name.Clear();
            pat_surname.Clear();
            pat_age.Clear();
            pat_city.Clear();
            pat_report.Clear();
            pat_approval.Text = "Added Successfully";
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            textBox1.Text = selectedRow.Cells[1].Value.ToString();
        }
    }
}
