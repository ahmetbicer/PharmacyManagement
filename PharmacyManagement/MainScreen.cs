using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagement
{
    public partial class MainScreen : Form
    {
        Medicine m = new Medicine();
        Patient p = new Patient();
        Pharmacy ph = new Pharmacy();
        MedicineFactories mf = new MedicineFactories();
        Employee emp = new Employee();
        Logs l = new Logs();
        Billing b = new Billing();

        BarcodeScanner bs = new BarcodeScanner();
        OpenFileDialog open = new OpenFileDialog();
        OpenFileDialog open1 = new OpenFileDialog();

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt13 = new DataTable();

        int[] index = new int[12];

        string globalID;
        string employeeName;

        string imgpath = "";
        string imgpath2 = "";

        bool isEmployee = false;

        public MainScreen(string username)
        {
            InitializeComponent();
            globalID = username;
            label76.Text = String.Format("Welcome to your pharmacy, {0}!", globalID);
        }

        public MainScreen(string empUsername, string pharmacyName)
        {
            InitializeComponent();
            employeeName = empUsername;
            globalID = pharmacyName;
            isEmployee = true;
            tabControl1.TabPages[1].Enabled = false;
            tabControl1.TabPages[4].Enabled = false;
            tabControl1.TabPages[5].Enabled = false;
            label76.Text = String.Format("Welcome to your pharmacy, {0}!", employeeName);
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(sender, e);
            tabControl4_SelectedIndexChanged(sender, e);
            tabControl5_SelectedIndexChanged(sender, e);

            dt3.Columns.Add("Name");
            dt3.Columns.Add("Stock");
            dt3.Columns.Add("Quantity");
            dt3.Columns.Add("Unit Price $");

            dt4.Columns.Add("Name");
            dt4.Columns.Add("Stock");
            dt4.Columns.Add("Quantity");
            dt4.Columns.Add("Unit Price $");

            dt7.Columns.Add("Name");
            dt7.Columns.Add("Stock");
            dt7.Columns.Add("Quantity");
            dt7.Columns.Add("Unit Price $");

            dt13.Columns.Add("Name");
            dt13.Columns.Add("Stock");
            dt13.Columns.Add("Quantity");
            dt13.Columns.Add("Unit Price $");

            dataGridView6.DataSource = dt3;
            dataGridView8.DataSource = dt4;
            dataGridView7.DataSource = dt7;
            dataGridView13.DataSource = dt13;

            foreach (DataGridViewColumn col1 in dataGridView6.Columns)
            {
                col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn col2 in dataGridView8.Columns)
            {
                col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn col3 in dataGridView7.Columns)
            {
                col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn col4 in dataGridView13.Columns)
            {
                col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn col5 in dataGridView16.Columns)
            {
                col5.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            pictureBox1.Hide();
            pictureBox4.Hide();
            pictureBox2.Hide();
            pictureBox6.Hide();

            textBox6.Text = globalID;
            textBox6.ReadOnly = true;

            textBox13.BackColor = System.Drawing.Color.FromArgb(30, 93, 185);
            textBox20.BackColor = System.Drawing.Color.FromArgb(30, 93, 185);
            textBox22.BackColor = System.Drawing.Color.FromArgb(30, 93, 185);
            textBox25.BackColor = System.Drawing.Color.FromArgb(30, 93, 185);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var l = new LoginScreen();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                sell_to_patient();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                buy_from_pharmacy();
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
                viewEmployees();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[5])
            {
                viewLogs();
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

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl4.SelectedTab == tabControl4.TabPages[0])
            {
                sell_to_patient();
            }
            if (tabControl4.SelectedTab == tabControl4.TabPages[1])
            {
                sell_to_pharmacy();
            }
        }

        private void tabControl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl5.SelectedTab == tabControl5.TabPages[0])
            {
                buy_from_pharmacy();
            }
            if (tabControl5.SelectedTab == tabControl5.TabPages[1])
            {
                buy_from_medicine_factories();
            }
        }

        private void tabControl6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl6.SelectedTab == tabControl6.TabPages[0])
            {
                viewEmployees();
            }
        }

        private void tabControl7_SelectedIndexChanged(object sender, EventArgs e)
        {
            button26.Enabled = false;

            if (tabControl7.SelectedTab == tabControl7.TabPages[0])
            {
                viewLogs();
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            }

            if (tabControl7.SelectedTab == tabControl7.TabPages[1])
            {
                getChartData();
            }

            if (tabControl7.SelectedTab == tabControl7.TabPages[2])
            {
                updatePharmacy();
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open.RestoreDirectory = false;
            open.ShowHelp = true;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = new Bitmap(open.FileName);
                imgpath = open.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            open1.RestoreDirectory = false;
            open1.ShowHelp = true;
            open1.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                pictureBox5.Image = new Bitmap(open1.FileName);
                imgpath2 = open1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                m.AddMedicine(name.Text, stock.Text, price.Text, dose.Text, "1", definition.Text, ingredients.Text, imgpath, globalID);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                m.AddMedicine(name.Text, stock.Text, price.Text, dose.Text, "0", definition.Text, ingredients.Text, imgpath, globalID);
            }

            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' added the medicine '{2}'", DateTime.Now.ToString(), employeeName, name.Text), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' added the medicine '{2}'", DateTime.Now.ToString(), globalID, name.Text), globalID);

            }
            name.Clear();
            stock.Clear();
            price.Clear();
            dose.Clear();
            definition.Clear();
            ingredients.Clear();
            comboBox1.ResetText();
            open.Reset();
            pictureBox3.Image = null;
            imgpath = "";
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
            if(comboBox3.SelectedIndex == 0)
            {
                p.addPatient(ID.Text, pat_name.Text, pat_surname.Text, pat_age.Text, pat_city.Text, "1", globalID);
            }
            if (comboBox3.SelectedIndex == 1)
            {
                p.addPatient(ID.Text, pat_name.Text, pat_surname.Text, pat_age.Text, pat_city.Text, "0", globalID);
            }
            
            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' added the patient '{2}'", DateTime.Now.ToString(), employeeName, pat_name.Text), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' added the patient '{2}'", DateTime.Now.ToString(), globalID, pat_name.Text), globalID);

            }
            ID.Clear();
            pat_name.Clear();
            pat_surname.Clear();
            pat_age.Clear();
            pat_city.Clear();
            comboBox3.ResetText();
            pat_approval.Text = "Added Successfully";
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // index2 = e.RowIndex;
            index[2] = e.RowIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = dataGridView3.Rows[index[0]];
            string medName = selectedRow.Cells[0].Value.ToString();
            int id = selectedRow.Index + 1;
            m.DeleteMedicine(medName, id, globalID);

            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' deleted the medicine '{2}'", DateTime.Now.ToString(), employeeName, medName), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' deleted the medicine '{2}'", DateTime.Now.ToString(), globalID, medName), globalID);

            }

            tabControl2.SelectedIndex = 1;
            tabControl2.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView3.Rows[index[0]];
            pictureBox5.Image = Image.FromFile(m.GetImagePath(selectedRow.Cells[0].Value.ToString(), globalID));
            textBox4.Text = selectedRow.Cells[0].Value.ToString();
            textBox16.Text = selectedRow.Cells[1].Value.ToString();
            textBox3.Text = selectedRow.Cells[2].Value.ToString();
            textBox5.Text = selectedRow.Cells[3].Value.ToString();
            richTextBox2.Text = selectedRow.Cells[4].Value.ToString();
            richTextBox1.Text = selectedRow.Cells[5].Value.ToString();
            if(selectedRow.Cells[6].Value.ToString() == "1")
            {
                comboBox2.SelectedIndex = 0;
            }
            if (selectedRow.Cells[6].Value.ToString() == "0")
            {
                comboBox2.SelectedIndex = 1;
            }
            //textBox2.Text = selectedRow.Cells[6].Value.ToString();
            tabControl2.SelectedIndex = 2;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = dataGridView3.Rows[index[0]];
            int indexInt = index[0] + 1;
            if (imgpath2 == "")
            {
                if(comboBox2.SelectedIndex == 0)
                {
                    m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, "1", globalID);
                }
                if (comboBox2.SelectedIndex == 1)
                {
                    m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, "0", globalID);
                }

            }
            else
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, "1", imgpath2, globalID);
                }
                if (comboBox2.SelectedIndex == 1)
                {
                    m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, "0", imgpath2, globalID);
                }
            }

            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' updated the medicine '{2}'", DateTime.Now.ToString(), employeeName, textBox4.Text), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' updated the medicine '{2}'", DateTime.Now.ToString(), globalID, textBox4.Text), globalID);

            }

            textBox4.Clear();
            textBox16.Clear();
            textBox3.Clear();
            textBox5.Clear();
            richTextBox2.Clear();
            richTextBox1.Clear();
            comboBox2.ResetText();
            pictureBox5.Image = null;
            imgpath2 = "";
            label17.Text = "Updated Successfully";
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
            DataGridViewRow selectedRow = dataGridView4.Rows[index[2]];
            string patID = selectedRow.Cells[1].Value.ToString();
            string patName = selectedRow.Cells[2].Value.ToString();
            string patSurname = selectedRow.Cells[3].Value.ToString();
            string patAge = selectedRow.Cells[4].Value.ToString();
            string patCity = selectedRow.Cells[5].Value.ToString();
            if(selectedRow.Cells[6].Value.ToString() == "1")
            {
                comboBox4.SelectedIndex = 0;
            }
            if (selectedRow.Cells[6].Value.ToString() == "0")
            {
                comboBox4.SelectedIndex = 1;
            }

            textBox7.Text = patID;
            textBox8.Text = patName;
            textBox10.Text = patSurname;
            textBox12.Text = patAge;
            textBox11.Text = patCity;


            tabControl3.SelectedIndex = 2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index[2]];
            string patPatId = selectedRow.Cells[0].Value.ToString();
            string patID = textBox7.Text;
            string patName = textBox8.Text;
            string patSurname = textBox10.Text;
            string patAge = textBox12.Text;
            string patCity = textBox11.Text;
            if(comboBox4.SelectedIndex == 0)
            {
                p.updatePatient(patPatId, patID, patName, patSurname, patAge, patCity, "1", globalID);
            }
            if (comboBox4.SelectedIndex == 1)
            {
                p.updatePatient(patPatId, patID, patName, patSurname, patAge, patCity, "0", globalID);
            }
            
            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' updated the patient '{2}'", DateTime.Now.ToString(), employeeName, textBox8.Text), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' updated the patient '{2}'", DateTime.Now.ToString(), globalID, textBox8.Text), globalID);

            }

            textBox7.Clear();
            textBox8.Clear();
            textBox10.Clear();
            textBox12.Clear();
            textBox11.Clear();
            comboBox4.ResetText();
            label22.Text = "Updated Successfully";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.Rows[index[2]];
            string patPatId = selectedRow.Cells[0].Value.ToString();
            p.deletePatient(patPatId, globalID);
            if (isEmployee)
            {
                l.AddLogs(String.Format("{0}  -  '{1}' deleted the patient '{2}'", DateTime.Now.ToString(), employeeName, patPatId), globalID);
            }
            else
            {
                l.AddLogs(String.Format("{0}  -  '{1}' deleted the patient '{2}'", DateTime.Now.ToString(), globalID, patPatId), globalID);

            }
            tabControl3.SelectedIndex = 1;
            tabControl3.SelectedIndex = 0;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[0] = e.RowIndex;
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text == "")//
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
            DataTable dt = m.CartMedicine(globalID);//
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
                                dt3.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, newQ, row.Cells[3].Value);
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
            if (e.ColumnIndex == 0 && e.RowIndex > -1)//
            {
                Rectangle rect = dataGridView5.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                try
                {
                    pictureBox1.Image = Image.FromFile(m.GetImagePath(dataGridView5.Rows[e.RowIndex].Cells[1].Value.ToString(), globalID));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox1.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox1.Location = new Point(rect.X + 471, rect.Y + 104);
                pictureBox1.Width = 200;
                pictureBox1.Height = 200;
                pictureBox1.Show();
            }
        }

        private void dataGridView5_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)//
            {
                pictureBox1.Hide();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView6.Rows.Count != 0)//
            {
                if (isEmployee)
                {
                    Checkout c = new Checkout(dt3, dataGridView9.Rows[index[4]].Cells[0].Value.ToString(), employeeName, globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                    c.ShowDialog();
                    sell_to_patient();
                    dt3.Rows.Clear();
                }
                else
                {
                    Checkout c = new Checkout(dt3, dataGridView9.Rows[index[4]].Cells[0].Value.ToString(), globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                    c.ShowDialog();
                    sell_to_patient();
                    dt3.Rows.Clear();
                }
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[3] = e.RowIndex;//
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (index[3] > -1 && dt3.Rows.Count != 0)//
            {
                dt3.Rows[index[3]].Delete();
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[4] = e.RowIndex;//
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;//
            tabControl3.SelectedIndex = 1;
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text == "")//
            {
                sell_to_patient();
            }

            DataTable dt = p.SearchPatient(textBox17.Text, globalID);
            dataGridView9.Columns.Clear();
            dataGridView9.DataSource = dt;
        }

        //sell to pharmacy
        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (textBox18.Text == "")
            {
                sell_to_pharmacy();
            }

            DataTable dt = ph.searchPharmacy(textBox18.Text);
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[5] = e.RowIndex;
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[6] = e.RowIndex;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (index[6] > -1 && dt4.Rows.Count != 0)
            {
                dt4.Rows[index[6]].Delete();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView8.Rows.Count != 0)
            {
                if (isEmployee)
                {
                    PharmacyCheckout c = new PharmacyCheckout(dt4, dataGridView1.Rows[index[5]].Cells[0].Value.ToString(), employeeName, globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                    c.ShowDialog();
                    sell_to_pharmacy();
                    dt4.Rows.Clear();
                }
                else
                {
                    PharmacyCheckout c = new PharmacyCheckout(dt4, dataGridView1.Rows[index[5]].Cells[0].Value.ToString(), globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                    c.ShowDialog();
                    sell_to_pharmacy();
                    dt4.Rows.Clear();
                }

            }
        }

        private void dataGridView10_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                Rectangle rect = dataGridView10.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                try
                {
                    pictureBox4.Image = Image.FromFile(m.GetImagePath(dataGridView10.Rows[e.RowIndex].Cells[1].Value.ToString(), globalID));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox4.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox4.Location = new Point(rect.X + 471, rect.Y + 104);
                pictureBox4.Width = 200;
                pictureBox4.Height = 200;
                pictureBox4.Show();
            }
        }

        private void dataGridView10_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                pictureBox4.Hide();
            }
        }

        private void dataGridView10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {

                foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                {
                    foreach (DataGridViewRow row2 in dataGridView8.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            int newQ = int.Parse(row.Cells[5].Value.ToString()) + int.Parse(row2.Cells[2].Value.ToString());
                            if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                            {
                                dt4.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, newQ, row.Cells[3].Value);
                                dt4.Rows[row2.Index].Delete();

                            }
                            row.Cells[5].Value = 0;
                            dataGridView8.DataSource = dt4;
                        }

                    }

                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        dt4.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[5].Value, row.Cells[3].Value);

                    }

                    row.Cells[5].Value = 0;
                    dataGridView8.DataSource = dt4;
                }

            }

            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                {
                    if (int.Parse(row.Cells[2].Value.ToString()) > int.Parse(row.Cells[5].Value.ToString()))
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) + 1;

                }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                {
                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) - 1;
                    }
                }
            }
        }

        public void sell_to_pharmacy()
        {
            DataTable dt = m.CartMedicine(globalID);
            dataGridView10.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView10.Columns.Add(col5);
            dataGridView10.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView10.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView10.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView10.Rows)
            {
                dataGridView10.Rows[row.Index].Cells[5].Value = 0;

            }

            DataTable dt5 = ph.cartPharmacy(globalID);
            dataGridView1.DataSource = dt5;
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if (textBox20.Text == "")
            {
                sell_to_pharmacy();
            }

            DataTable dt = m.SearchMedicine(textBox20.Text, globalID);
            dataGridView10.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView10.Columns.Add(col5);

            dataGridView10.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView10.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView10.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView10.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView10.Rows)
            {
                dataGridView10.Rows[row.Index].Cells[5].Value = 0;
            }
        }

        //buy from pharmacy
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                buy_from_pharmacy();
            }

            DataTable dt = ph.searchPharmacy(textBox14.Text);
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = dt;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[7] = e.RowIndex;
            changePharmacyMed();
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[8] = e.RowIndex;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (index[8] > -1 && dt7.Rows.Count != 0)
            {
                dt7.Rows[index[8]].Delete();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView7.Rows.Count != 0)
            {
                BuyPharmacyCheckout c = new BuyPharmacyCheckout(dt7, dataGridView2.Rows[index[7]].Cells[0].Value.ToString(), globalID);
                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                c.ShowDialog();
                buy_from_pharmacy();
                dt7.Rows.Clear();
            }
        }

        private void dataGridView11_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                Rectangle rect = dataGridView11.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                try
                {
                    pictureBox2.Image = Image.FromFile(m.GetImagePath(dataGridView11.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView2.Rows[index[7]].Cells[0].Value.ToString()));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox2.Location = new Point(rect.X + 471, rect.Y + 104);
                pictureBox2.Width = 200;
                pictureBox2.Height = 200;
                pictureBox2.Show();
            }
        }

        private void dataGridView11_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                pictureBox2.Hide();
            }
        }

        private void dataGridView11_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {

                foreach (DataGridViewRow row in dataGridView11.SelectedRows)
                {
                    foreach (DataGridViewRow row2 in dataGridView7.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            int newQ = int.Parse(row.Cells[5].Value.ToString()) + int.Parse(row2.Cells[2].Value.ToString());
                            if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                            {
                                dt7.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, newQ, row.Cells[3].Value);
                                dt7.Rows[row2.Index].Delete();

                            }
                            row.Cells[5].Value = 0;
                            dataGridView7.DataSource = dt7;
                        }

                    }

                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        dt7.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[5].Value, row.Cells[3].Value);

                    }

                    row.Cells[5].Value = 0;
                    dataGridView7.DataSource = dt7;
                }

            }

            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView11.SelectedRows)
                {
                    if (int.Parse(row.Cells[2].Value.ToString()) > int.Parse(row.Cells[5].Value.ToString()))
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) + 1;

                }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView11.SelectedRows)
                {
                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) - 1;
                    }
                }
            }
        }

        private void buy_from_pharmacy()
        {
            DataTable dt5 = ph.cartPharmacy(globalID);
            dataGridView2.DataSource = dt5;

            DataTable dt = m.CartMedicine(dataGridView2.Rows[index[7]].Cells[0].Value.ToString());
            dataGridView11.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView11.Columns.Add(col5);
            dataGridView11.DataSource = dt;


            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView11.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView11.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView11.Rows)
            {
                dataGridView11.Rows[row.Index].Cells[5].Value = 0;

            }
        }

        private void changePharmacyMed()
        {
            if (index[7] > -1)
            {
                DataTable dt = m.CartMedicine(dataGridView2.Rows[index[7]].Cells[0].Value.ToString());
                //DataTable dt = m.CartMedicine(globalID);
                dataGridView11.Columns.Clear();

                DataGridViewImageColumn col5 = new DataGridViewImageColumn();
                col5.Name = "Img";
                col5.HeaderText = "Img";
                col5.Image = Properties.Resources.image;
                dataGridView11.Columns.Add(col5);
                dataGridView11.DataSource = dt;

                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                col1.HeaderText = "-";
                col1.Text = "-";
                col1.Width = 30;
                col1.UseColumnTextForButtonValue = true;
                dataGridView11.Columns.Add(col1);
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Quantity";
                col3.Width = 70;
                dataGridView11.Columns.Add(col3);
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                col2.HeaderText = "+";
                col2.Text = "+";
                col2.Width = 30;
                col2.UseColumnTextForButtonValue = true;
                dataGridView11.Columns.Add(col2);
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.HeaderText = "Add to Cart";
                col.Text = "Add";
                col.UseColumnTextForButtonValue = true;
                dataGridView11.Columns.Add(col);

                foreach (DataGridViewColumn column in dataGridView11.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewRow row in dataGridView11.Rows)
                {
                    dataGridView11.Rows[row.Index].Cells[5].Value = 0;

                }
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            if (textBox22.Text == "")
            {
                buy_from_pharmacy();
            }

            DataTable dt = m.SearchMedicine(textBox22.Text, dataGridView2.Rows[index[7]].Cells[0].Value.ToString());
            dataGridView11.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView11.Columns.Add(col5);

            dataGridView11.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView11.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView11.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView11.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView11.Rows)
            {
                dataGridView11.Rows[row.Index].Cells[5].Value = 0;
            }
        }

        //buy from medicine factories
        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            if (textBox23.Text == "")
            {
                buy_from_medicine_factories();
            }

            DataTable dt = mf.searchFactory(textBox23.Text);
            dataGridView12.Columns.Clear();
            dataGridView12.DataSource = dt;
        }

        private void dataGridView12_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[9] = e.RowIndex;
            changeFactoryMed();
        }

        private void dataGridView13_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[10] = e.RowIndex;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (index[10] > -1 && dt13.Rows.Count != 0)
            {
                dt13.Rows[index[10]].Delete();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (dataGridView13.Rows.Count != 0)
            {
                BuyFactoryCheckout c = new BuyFactoryCheckout(dt13, dataGridView12.Rows[index[9]].Cells[0].Value.ToString(), globalID);
                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 35, this.Location.Y + 65);
                c.ShowDialog();
                buy_from_medicine_factories();
                dt13.Rows.Clear();
            }
        }

        private void dataGridView14_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                Rectangle rect = dataGridView14.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                try
                {
                    pictureBox6.Image = Image.FromFile(m.GetFactoryImagePath(dataGridView14.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView12.Rows[index[9]].Cells[0].Value.ToString()));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox6.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox6.Location = new Point(rect.X + 471, rect.Y + 104);
                pictureBox6.Width = 200;
                pictureBox6.Height = 200;
                pictureBox6.Show();
            }
        }

        private void dataGridView14_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                pictureBox6.Hide();
            }
        }

        private void dataGridView14_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {

                foreach (DataGridViewRow row in dataGridView14.SelectedRows)
                {
                    foreach (DataGridViewRow row2 in dataGridView13.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            int newQ = int.Parse(row.Cells[5].Value.ToString()) + int.Parse(row2.Cells[2].Value.ToString());
                            if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                            {
                                dt13.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, newQ, row.Cells[3].Value);
                                dt13.Rows[row2.Index].Delete();

                            }
                            row.Cells[5].Value = 0;
                            dataGridView13.DataSource = dt13;
                        }

                    }

                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        dt13.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[5].Value, row.Cells[3].Value);

                    }

                    row.Cells[5].Value = 0;
                    dataGridView13.DataSource = dt13;
                }

            }

            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView14.SelectedRows)
                {
                    if (int.Parse(row.Cells[2].Value.ToString()) > int.Parse(row.Cells[5].Value.ToString()))
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) + 1;

                }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView14.SelectedRows)
                {
                    if (int.Parse(row.Cells[5].Value.ToString()) != 0)
                    {
                        row.Cells[5].Value = int.Parse(row.Cells[5].Value.ToString()) - 1;
                    }
                }
            }
        }

        private void buy_from_medicine_factories()
        {
            DataTable dt5 = mf.cartFactory();
            dataGridView12.DataSource = dt5;

            DataTable dt = m.CartFactory(dataGridView12.Rows[index[9]].Cells[0].Value.ToString());
            dataGridView14.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView14.Columns.Add(col5);
            dataGridView14.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView14.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView14.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView14.Rows)
            {
                dataGridView14.Rows[row.Index].Cells[5].Value = 0;

            }
        }

        private void changeFactoryMed()
        {
            if (index[9] > -1)
            {
                DataTable dt = m.CartFactory(dataGridView12.Rows[index[9]].Cells[0].Value.ToString());
                dataGridView14.Columns.Clear();

                DataGridViewImageColumn col5 = new DataGridViewImageColumn();
                col5.Name = "Img";
                col5.HeaderText = "Img";
                col5.Image = Properties.Resources.image;
                dataGridView14.Columns.Add(col5);
                dataGridView14.DataSource = dt;

                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                col1.HeaderText = "-";
                col1.Text = "-";
                col1.Width = 30;
                col1.UseColumnTextForButtonValue = true;
                dataGridView14.Columns.Add(col1);
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Quantity";
                col3.Width = 70;
                dataGridView14.Columns.Add(col3);
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                col2.HeaderText = "+";
                col2.Text = "+";
                col2.Width = 30;
                col2.UseColumnTextForButtonValue = true;
                dataGridView14.Columns.Add(col2);
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.HeaderText = "Add to Cart";
                col.Text = "Add";
                col.UseColumnTextForButtonValue = true;
                dataGridView14.Columns.Add(col);

                foreach (DataGridViewColumn column in dataGridView14.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewRow row in dataGridView14.Rows)
                {
                    dataGridView14.Rows[row.Index].Cells[5].Value = 0;

                }

            }
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            if (textBox25.Text == "")
            {
                buy_from_medicine_factories();
            }

            //DataTable dt = m.SearchMedicine(textBox25.Text, globalID);
            DataTable dt = m.SearchFactoryMedicine(textBox25.Text, dataGridView12.Rows[index[9]].Cells[0].Value.ToString());
            dataGridView14.Columns.Clear();

            DataGridViewImageColumn col5 = new DataGridViewImageColumn();
            col5.Name = "Img";
            col5.HeaderText = "Img";
            col5.Image = Properties.Resources.image;
            dataGridView14.Columns.Add(col5);

            dataGridView14.DataSource = dt;

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.HeaderText = "-";
            col1.Text = "-";
            col1.Width = 30;
            col1.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col1);
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Quantity";
            col3.Width = 70;
            dataGridView14.Columns.Add(col3);
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            col2.HeaderText = "+";
            col2.Text = "+";
            col2.Width = 30;
            col2.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col2);
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Add to Cart";
            col.Text = "Add";
            col.UseColumnTextForButtonValue = true;
            dataGridView14.Columns.Add(col);

            foreach (DataGridViewColumn column in dataGridView14.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in dataGridView14.Rows)
            {
                dataGridView14.Rows[row.Index].Cells[5].Value = 0;
            }
        }

        //employees
        private void button24_Click(object sender, EventArgs e)
        {
            emp.AddEmployee(textBox24.Text, textBox28.Text, textBox34.Text, textBox26.Text, textBox21.Text, textBox37.Text, textBox27.Text, globalID);
            textBox24.Clear();
            textBox28.Clear();
            textBox34.Clear();
            textBox26.Clear();
            textBox21.Clear();
            textBox37.Clear();
            textBox27.Clear();
            label63.ForeColor = Color.Green;
            label63.Text = "Added Succesfully";
        }

        private void viewEmployees()
        {
            dataGridView15.DataSource = emp.viewEmployees(globalID);
            DataGridViewColumn column = dataGridView15.Columns[0];
            column.Width = 120;
            dataGridView15.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void dataGridView15_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index[11] = e.RowIndex;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView15.Rows[index[11]];
            textBox29.Text = selectedRow.Cells[0].Value.ToString();
            textBox32.Text = selectedRow.Cells[1].Value.ToString();
            textBox33.Text = selectedRow.Cells[2].Value.ToString();
            textBox30.Text = selectedRow.Cells[3].Value.ToString();
            textBox36.Text = selectedRow.Cells[4].Value.ToString();
            textBox35.Text = selectedRow.Cells[5].Value.ToString();
            textBox31.Text = "*********";
            tabControl6.SelectedIndex = 2;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            emp.UpdateEmployee(textBox29.Text, textBox32.Text, textBox33.Text, textBox30.Text, textBox36.Text, textBox35.Text, textBox31.Text);
            textBox29.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox30.Clear();
            textBox36.Clear();
            textBox35.Clear();
            textBox31.Clear();
            label64.ForeColor = Color.Green;
            label64.Text = "Updated Successfully";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView15.Rows[index[11]];
            string EmployeeID = selectedRow.Cells[0].Value.ToString();
            emp.deleteEmployee(EmployeeID);
            tabControl6.SelectedIndex = 1;
            tabControl6.SelectedIndex = 0;
        }

        //settings
        private void button26_Click(object sender, EventArgs e)
        {
            ph.DeletePharmacy(globalID);
            this.Hide();
            var l = new LoginScreen();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            int r = ph.Login(textBox6.Text, textBox1.Text);
            if (r == 1)
            {
                button26.Enabled = true;
                textBox1.Text = "";
            }
            if (r == 0)
            {
                MessageBox.Show("Username or password is wrong!");
            }
        }

        private void updatePharmacy()
        {
            DataTable dt = ph.getPharmacyInfo(globalID);
            username.Text = globalID;
            email.Text = dt.Rows[0].ItemArray[0].ToString();
            password.Text = dt.Rows[0].ItemArray[1].ToString();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            if (reg.IsMatch(email.Text))
            {
                ph.updatePharmacy(email.Text, password.Text, globalID);
                tabControl7.SelectedIndex = 2;
                tabControl7.SelectedIndex = 1;
                label74.ForeColor = Color.Green;
                label74.Text = "Updated Successfully";
            }
            else
            {
                label74.ForeColor = Color.Red;
                label74.Text = "Invalid email adress!";
            }
                
            
        }

        private void viewLogs()
        {
            DataTable dt = l.SeeLogs(globalID);
            dataGridView16.DataSource = dt;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            bs.StartPosition = FormStartPosition.Manual;
            bs.Location = new Point(this.Location.X + 1070, this.Location.Y + 225);
            var result = bs.ShowDialog();
            if (result == DialogResult.OK)
            {
                string val = bs.decodedstr;

                string[] tokens = val.Split(',');
                name.Text = tokens[0];
                dose.Text = tokens[1];
                if(tokens[2] == "1")
                {
                    comboBox1.SelectedIndex = 0;
                }
                if (tokens[2] == "0")
                {
                    comboBox1.SelectedIndex = 1;
                }
                //report.Text = tokens[2];
                definition.Text = tokens[3];
                ingredients.Text = tokens[4];
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (m.checkPrescription(textBox15.Text))
            {
                DataTable dt16 = m.ReadPrescription(textBox15.Text, globalID);
                string patientid = m.PrescriptionPatient(textBox15.Text);

                if (isEmployee)
                {

                    Checkout c = new Checkout(dt16, patientid, employeeName, globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                    c.ShowDialog();

                    dt3.Rows.Clear();
                }
                else
                {
                    Checkout c = new Checkout(dt16, patientid, globalID);

                    c.StartPosition = FormStartPosition.Manual;
                    c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                    c.ShowDialog();

                    dt3.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Can't found a match for this prescriptionID!");
            }
            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private bool mouseDown;

        private Point lastLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void report_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pat_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pat_report_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void getChartData()
        {
            DataTable dt1 = b.getGraphData("2019", "11",globalID);
            DataTable dt2 = b.getGraphData("2019", "12",globalID);

            chart1.Series["Expense"].Points.AddXY(11, dt1.Rows[0].ItemArray[1]);
            chart1.Series["Income"].Points.AddXY(11, dt1.Rows[0].ItemArray[0]);

            chart1.Series["Expense"].Points.AddXY(12, dt2.Rows[0].ItemArray[1]);
            chart1.Series["Income"].Points.AddXY(12, dt2.Rows[0].ItemArray[0]);

            chart1.Series["Expense"]["PixelPointWidth"] = "80";
            chart1.Series["Income"]["PixelPointWidth"] = "80";

            chart1.Series["Expense"].IsValueShownAsLabel = true;
            chart1.Series["Income"].IsValueShownAsLabel = true;
        }
    }
}
