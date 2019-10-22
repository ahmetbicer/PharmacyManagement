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
        Medicine m = new Medicine();
        int index;
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


        private void button3_Click(object sender, EventArgs e)
        {
            m.AddMedicine(name.Text, stock.Text, report.Text, usage.Text);
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
            index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            textBox1.Text = selectedRow.Cells[1].Value.ToString();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            string medName = selectedRow.Cells[1].Value.ToString();
            m.DeleteMedicine(medName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = m.ViewMedicine();
            dataGridView3.DataSource = dt;
            DataGridViewColumn column = dataGridView3.Columns[0];
            column.Width = 150;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            string medName = selectedRow.Cells[1].Value.ToString();
            string medStock = selectedRow.Cells[2].Value.ToString();
            string medReport = selectedRow.Cells[3].Value.ToString();
            string medUsage = selectedRow.Cells[4].Value.ToString();

            textBox3.Text = medName;
            textBox5.Text = medStock;
            textBox4.Text = medReport;
            textBox2.Text = medUsage;

            tabControl2.SelectedIndex = 2;
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            string medId = selectedRow.Cells[0].Value.ToString();
            string medName = textBox3.Text;
            string medStock = textBox5.Text;
            string medReport = textBox4.Text;
            string medUsage = textBox2.Text;
            m.UpdateMedicine(medId, medName, medStock, medReport,medUsage);
            textBox3.Clear();
            textBox5.Clear();
            textBox4.Clear();
            textBox2.Clear();
            label14.Text = "Updated Successfully";
        }
    }
}