using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        int index4;
        string globalID;
        int sellIndex;
        bool clicked = false;

        public MainScreen(string username)
        {
            InitializeComponent();
            globalID = username;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(sender, e);
            dt3.Columns.Add("Name");
            dt3.Columns.Add("Stock");
            dt3.Columns.Add("Quantity");
            dt3.Columns.Add("Unit Price $");

            dataGridView6.DataSource = dt3;

            foreach (DataGridViewColumn col1 in dataGridView6.Columns)
            {
                col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            pictureBox1.Hide();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var l = new Login();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings se = new Settings();
            se.Closed += (s, args) => this.Close();
            se.StartPosition = FormStartPosition.Manual;
            se.Location = new Point(this.Location.X, this.Location.Y);
            se.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                sell_to_patient();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[2])
            {
                viewMedicines();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[3])
            {
                viewPatients();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[4])
            {

            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabControl2.TabPages[0])
            {
                viewMedicines();
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl3.SelectedTab == tabControl3.TabPages[0])
            {
                viewPatients();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m.AddMedicine(name.Text, stock.Text, report.Text, usage.Text, textBox16.Text, globalID);
            name.Clear();
            stock.Clear();
            report.Clear();
            usage.Clear();
            textBox16.Clear();
            approval.Text = "Added Successfully";
        }

        private void viewPatients()
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
            m.DeleteMedicine(medName, globalID);
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
            m.UpdateMedicine(medId, medName, medStock, medReport, medUsage, globalID);
            textBox3.Clear();
            textBox5.Clear();
            textBox4.Clear();
            textBox2.Clear();
            label14.Text = "Updated Successfully";
        }

        private void viewMedicines()
        {
            DataTable dt = m.ViewMedicine(globalID);
            dataGridView3.DataSource = dt;
            DataGridViewColumn column = dataGridView3.Columns[0];
            column.Width = 150;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            string patID = selectedRow.Cells[1].Value.ToString();
            string patName = selectedRow.Cells[2].Value.ToString();
            string patSurname = selectedRow.Cells[3].Value.ToString();
            string patAge = selectedRow.Cells[4].Value.ToString();
            string patCity = selectedRow.Cells[5].Value.ToString();
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
                sell_to_patient();
            }

            DataTable dt = m.SearchMedicine(textBox13.Text, globalID);
            dataGridView5.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView5.Columns.Add(col5);

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
                dataGridView5.Rows[row.Index].Cells[5].Value = 0;
            }
        }

        public void sell_to_patient()
        {
            DataTable dt = m.CartMedicine(globalID);
            dataGridView5.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView5.Columns.Add(col5);
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
                dataGridView5.Rows[row.Index].Cells[5].Value = 0;

            }

            DataTable dt5 = p.cartPatients(globalID);
            dataGridView9.DataSource = dt5;
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sellIndex = e.RowIndex;

            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {

                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    foreach (DataGridViewRow row2 in dataGridView6.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            int newQ = int.Parse(row.Cells[5].Value.ToString()) + int.Parse(row2.Cells[2].Value.ToString());
                            if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                            {
                                dt3.Rows.Add(row.Cells[5].Value, row.Cells[2].Value, newQ, row.Cells[3].Value);
                                dt3.Rows[row2.Index].Delete();

                            }
                            row.Cells[5].Value = 0;
                            dataGridView6.DataSource = dt3;
                        }

                    }

                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        dt3.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[5].Value, row.Cells[3].Value);

                    }

                    row.Cells[5].Value = 0;
                    dataGridView6.DataSource = dt3;
                }

            }

            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    if (int.Parse(row.Cells[2].Value.ToString()) > int.Parse(row.Cells[5].Value.ToString()))
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) + 1;

                }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) - 1;
                    }
                }
            }
        }

        private void dataGridView5_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                Rectangle rect = dataGridView5.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                try
                {
                    pictureBox1.Image = Image.FromFile(String.Format(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\Med{0}.png", e.RowIndex.ToString()));
                }
                catch(System.IO.FileNotFoundException)
                {
                    pictureBox1.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\Med1.png");
                }
                
                pictureBox1.Location = new Point(rect.X + 353, rect.Y + 89);
                pictureBox1.Width = 100;
                pictureBox1.Height = 100;
                pictureBox1.Show();
            }
        }

        private void dataGridView5_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                pictureBox1.Hide();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView6.Rows.Count != 0)
            {
                Checkout c = new Checkout(dt3, dataGridView9.Rows[index4].Cells[0].Value.ToString(), globalID);

                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                c.ShowDialog();

                dt3.Rows.Clear();
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index3 = e.RowIndex;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (index3 > -1 && dt3.Rows.Count != 0)
            {
                dt3.Rows[index3].Delete();
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index4 = e.RowIndex;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            tabControl3.SelectedIndex = 1;
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text == "")
            {
                sell_to_patient();
            }

            DataTable dt = p.SearchPatient(textBox17.Text, globalID);
            dataGridView9.Columns.Clear();
            dataGridView9.DataSource = dt;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index2 = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView4.Rows[index2];
            textBox6.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = new Bitmap(open.FileName);
                pictureBox3.Image.Save(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\med12.png");
            }
        }
    }
}
