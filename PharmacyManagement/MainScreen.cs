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
        Patient p = new Patient();
        Sell s = new Sell();

        int index;
        int index2;
        string globalID;
        int sellIndex;

        public MainScreen()
        {
            InitializeComponent();
        }

        public MainScreen(string username)
        {
            InitializeComponent();
            globalID = username;
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
                    using (var cmd = new SQLiteCommand("SELECT Username FROM pharmacyList", conn))
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
                    using (var cmd = new SQLiteCommand("SELECT Username FROM pharmacyList", conn))
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
            m.AddMedicine(name.Text, stock.Text, report.Text, usage.Text,globalID);
            name.Clear();
            stock.Clear();
            report.Clear();
            usage.Clear();
            approval.Text = "Added Successfully";
        }

        private void tabPage7_MouseEnter(object sender, EventArgs e)
        {

            dataGridView4.DataSource = p.viewPatients(globalID);
            DataGridViewColumn column = dataGridView4.Columns[0];
            column.Width = 120;
            dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.addPatient(ID.Text, pat_name.Text, pat_surname.Text, pat_age.Text, pat_city.Text, pat_report.Text, globalID);
            ID.Clear();
            pat_name.Clear();
            pat_surname.Clear();
            pat_age.Clear();
            pat_city.Clear();
            pat_report.Clear();
            pat_approval.Text = "Added Successfully";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            string medName = selectedRow.Cells[1].Value.ToString();
            m.DeleteMedicine(medName,globalID);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = m.ViewMedicine(globalID);
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
            m.UpdateMedicine(medId, medName, medStock, medReport,medUsage,globalID);
            textBox3.Clear();
            textBox5.Clear();
            textBox4.Clear();
            textBox2.Clear();
            label14.Text = "Updated Successfully";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button5_Click(sender,e);
            tabPage7_MouseEnter(sender, e);

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index2 = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            textBox6.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            string patID = selectedRow.Cells[1].Value.ToString();
            string patName = selectedRow.Cells[2].Value.ToString();
            string patSurname = selectedRow.Cells[3].Value.ToString();
            string patAge = selectedRow.Cells[4].Value.ToString();
            string patCity= selectedRow.Cells[5].Value.ToString();
            string patHaveReport = selectedRow.Cells[6].Value.ToString();

            textBox7.Text = patID;
            textBox8.Text = patName;
            textBox10.Text = patSurname;
            textBox12.Text = patAge;
            textBox11.Text = patCity;
            textBox9.Text = patHaveReport;


            tabControl3.SelectedIndex = 2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            string patPatId = selectedRow.Cells[0].Value.ToString();
            string patID = textBox7.Text;
            string patName = textBox8.Text;
            string patSurname = textBox10.Text;
            string patAge = textBox12.Text;
            string patCity = textBox11.Text;
            string patHaveReport = textBox9.Text;
            p.updatePatient(patPatId, patID, patName, patSurname, patAge, patCity, patHaveReport, globalID);
            textBox7.Clear();
            textBox8.Clear();
            textBox10.Clear();
            textBox12.Clear();
            textBox11.Clear();
            textBox9.Clear();
            label22.Text = "Updated Successfully";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            string patPatId = selectedRow.Cells[0].Value.ToString();
            p.deletePatient(patPatId, globalID);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            textBox1.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                tabControl1_Enter(sender, e);
            }

            DataTable dt = m.SearchMedicine(textBox13.Text, globalID);
            dataGridView5.DataSource = dt;
            DataGridViewColumn column = dataGridView5.Columns[0];
            column.Width = 150;
            dataGridView5.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            DataTable dt = m.ViewMedicine(globalID);
            dataGridView5.DataSource = dt;
            DataGridViewColumn column = dataGridView5.Columns[0];
            column.Width = 150;
            dataGridView5.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sellIndex = e.RowIndex;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView5.Rows[sellIndex];
            string sellname = selectedRow.Cells[1].Value.ToString();
            int stock = int.Parse(selectedRow.Cells[2].Value.ToString());
            s.SellMedicine(sellname, stock, globalID);
            tabControl1_Enter(sender, e);
        }
    }
}