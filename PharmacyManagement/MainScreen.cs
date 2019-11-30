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
        DataTable dt3 = new DataTable();


        int index;
        int index2;
        int index3;
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

            dt3.Columns.Add("Name");
            dt3.Columns.Add("Stock");
            dt3.Columns.Add("Quantity");
            dt3.Columns.Add("Unit Price $");

            dataGridView6.DataSource = dt3;


            DataGridViewColumn column = dataGridView6.Columns[0];
            column.Width = 59;
            DataGridViewColumn column2 = dataGridView6.Columns[1];
            column2.Width = 55;
            DataGridViewColumn column3 = dataGridView6.Columns[2];
            column3.Width = 64;
            DataGridViewColumn column4 = dataGridView6.Columns[3];
            column4.Width = 59;

            foreach(DataGridViewColumn col1 in dataGridView6.Columns)
            {
                col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
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
        
        private void button3_Click(object sender, EventArgs e)
        {
            m.AddMedicine(name.Text, stock.Text, report.Text, usage.Text,textBox16.Text,globalID);
            name.Clear();
            stock.Clear();
            report.Clear();
            usage.Clear();
            textBox16.Clear();
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
            tabControl1_Enter(sender, e);
            getpatients();

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
            dataGridView5.Columns.Clear();
            dataGridView5.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView5.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView5.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                dataGridView5.Rows[row.Index].Cells[4].Value = 0;
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {   
            DataTable dt = m.CartMedicine(globalID);
            dataGridView5.Columns.Clear();
            dataGridView5.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView5.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView5.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                dataGridView5.Rows[row.Index].Cells[4].Value = 0;

            }
        }

        
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sellIndex = e.RowIndex;

            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {

                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    foreach (DataGridViewRow row2 in dataGridView6.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            int newQ = int.Parse(row.Cells[4].Value.ToString()) + int.Parse(row2.Cells[2].Value.ToString());
                            if (int.Parse(row.Cells[4].Value.ToString()) != 0)
                            {
                                dt3.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, newQ, row.Cells[2].Value);
                                dt3.Rows[row2.Index].Delete();

                            }
                            row.Cells[4].Value = 0;
                            dataGridView6.DataSource = dt3;
                        }
                        
                    }
                    
                    if (int.Parse(row.Cells[4].Value.ToString()) != 0)
                    {
                        dt3.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[4].Value, row.Cells[2].Value);
                    
                    }

                    row.Cells[4].Value = 0;
                    dataGridView6.DataSource = dt3;
                }
                
            }

            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    row.Cells[4].Value = int.Parse(row.Cells[4].Value.ToString()) + 1;

                }
            }

            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    if(int.Parse(row.Cells[4].Value.ToString()) != 0)
                    {
                        row.Cells[4].Value = int.Parse(row.Cells[4].Value.ToString()) - 1;
                    }
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings se = new Settings();
            se.Closed += (s, args) => this.Close();
            se.StartPosition = FormStartPosition.Manual;
            se.Location = new Point(this.Location.X, this.Location.Y);
            se.Show();

        }

        private void getpatients()
        {
            DataTable dt = p.viewPatients(globalID);
            dataGridView9.DataSource = dt;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            getpatients();
        }

       private void button14_Click(object sender, EventArgs e)
        {
            /*foreach (DataGridViewRow row in dataGridView6.Rows)
            { 
                s.SellMedicine(row.Cells[0].Value.ToString(), int.Parse(row.Cells[1].Value.ToString()), int.Parse(row.Cells[2].Value.ToString()),globalID);                
            }*/

            dt3.Rows.Clear(); 
            
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index3 = e.RowIndex;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if(index3 > -1)
            {
                dt3.Rows[index3].Delete();
            }
        }
    }
}