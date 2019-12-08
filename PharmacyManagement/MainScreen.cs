﻿using System;
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
        Pharmacy ph = new Pharmacy();
        MedicineFactories mf = new MedicineFactories();
        Employee emp = new Employee();

        OpenFileDialog open = new OpenFileDialog();
        OpenFileDialog open1 = new OpenFileDialog();

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt13 = new DataTable();

        int index;
        int index2;
        int index3;
        int index4;
        int index5;
        int index6;
        int index7;
        int index8;
        int index9;
        int index10;
        int index11;

        string globalID;
        string employeeName;
        int sellIndex;
        int sellIndex2;
        int sellIndex3;
        int sellIndex4;

        string imgpath;
        string imgpath2;


        public MainScreen(string username)
        {
            InitializeComponent();
            globalID = username;
        }

        public MainScreen(string empUsername,string pharmacyName)
        {
            InitializeComponent();
            employeeName = empUsername;
            globalID = pharmacyName;
            tabControl1.TabPages[1].Enabled = false;
            tabControl1.TabPages[4].Enabled = false;
            tabControl1.TabPages[5].Enabled = false;
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


            pictureBox1.Hide();
            pictureBox4.Hide();
            pictureBox2.Hide();
            pictureBox6.Hide();

            textBox6.Text = globalID;
            textBox6.ReadOnly = true;
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
                updatePharmacy();
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
            if(tabControl6.SelectedTab == tabControl6.TabPages[0])
            {
                viewEmployees();
            }
        }

        private void tabControl7_SelectedIndexChanged(object sender, EventArgs e)
        {
            button26.Enabled = false;
            if(tabControl7.SelectedTab == tabControl7.TabPages[1])
            {
                updatePharmacy();
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
            m.AddMedicine(name.Text, stock.Text, price.Text,dose.Text,report.Text,definition.Text,ingredients.Text, imgpath, globalID);
            name.Clear();
            stock.Clear();
            price.Clear();
            dose.Clear();
            report.Clear();
            definition.Clear();
            ingredients.Clear();
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
            p.addPatient(ID.Text, pat_name.Text, pat_surname.Text, pat_age.Text, pat_city.Text, pat_report.Text, globalID);
            ID.Clear();
            pat_name.Clear();
            pat_surname.Clear();
            pat_age.Clear();
            pat_city.Clear();
            pat_report.Clear();
            pat_approval.Text = "Added Successfully";
        }


        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index2 = e.RowIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            string medName = selectedRow.Cells[0].Value.ToString();
            int id = selectedRow.Index + 1;
            m.DeleteMedicine(medName,id, globalID);
            tabControl2.SelectedIndex = 1;
            tabControl2.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            pictureBox5.Image = Image.FromFile(m.GetImagePath(selectedRow.Cells[0].Value.ToString(),globalID));
            textBox4.Text = selectedRow.Cells[0].Value.ToString();
            textBox16.Text = selectedRow.Cells[1].Value.ToString();
            textBox3.Text = selectedRow.Cells[2].Value.ToString();
            textBox5.Text = selectedRow.Cells[3].Value.ToString();
            richTextBox2.Text = selectedRow.Cells[4].Value.ToString();
            richTextBox1.Text = selectedRow.Cells[5].Value.ToString();
            textBox2.Text = selectedRow.Cells[6].Value.ToString();
            tabControl2.SelectedIndex = 2;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = dataGridView3.Rows[index];
            int indexInt = index + 1;
            if(imgpath2 == "")
            {
                m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, textBox2.Text, globalID);
            }
            else
            {
                m.UpdateMedicine(indexInt.ToString(), textBox4.Text, textBox16.Text, textBox3.Text, textBox5.Text, richTextBox2.Text, richTextBox1.Text, textBox2.Text, imgpath2, globalID);
            }
            
            textBox4.Clear();
            textBox16.Clear();
            textBox3.Clear();
            textBox5.Clear();
            richTextBox2.Clear();
            richTextBox1.Clear();
            textBox2.Clear();
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
            sellIndex = e.RowIndex;//

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
                    pictureBox1.Image = Image.FromFile(m.GetImagePath(dataGridView5.Rows[e.RowIndex].Cells[1].Value.ToString(),globalID));
                }
                catch(System.IO.FileNotFoundException)
                {
                    pictureBox1.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }
                
                pictureBox1.Location = new Point(rect.X + 353, rect.Y + 89);
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
                Checkout c = new Checkout(dt3, dataGridView9.Rows[index4].Cells[0].Value.ToString(), globalID);

                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                c.ShowDialog();

                dt3.Rows.Clear();
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index3 = e.RowIndex;//
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (index3 > -1 && dt3.Rows.Count != 0)//
            {
                dt3.Rows[index3].Delete();
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index4 = e.RowIndex;//
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
            index5 = e.RowIndex;
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index6 = e.RowIndex;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (index6 > -1 && dt4.Rows.Count != 0)
            {
                dt4.Rows[index6].Delete();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView8.Rows.Count != 0)
            {
                PharmacyCheckout c = new PharmacyCheckout(dt4, dataGridView1.Rows[index5].Cells[0].Value.ToString(),globalID);

                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                c.ShowDialog();

                dt4.Rows.Clear();
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

                pictureBox4.Location = new Point(rect.X + 353, rect.Y + 89);
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
        }//

        private void dataGridView10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sellIndex2 = e.RowIndex;

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
        }//

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
            index7 = e.RowIndex;
            changePharmacyMed();
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index8 = e.RowIndex;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (index8 > -1 && dt7.Rows.Count != 0)
            {
                dt7.Rows[index8].Delete();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView7.Rows.Count != 0)
            {
                BuyPharmacyCheckout c = new BuyPharmacyCheckout(dt7,dataGridView2.Rows[index7].Cells[0].Value.ToString(),globalID);
                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                c.ShowDialog();

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
                    pictureBox2.Image = Image.FromFile(m.GetImagePath(dataGridView11.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView2.Rows[index7].Cells[0].Value.ToString()));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox2.Location = new Point(rect.X + 353, rect.Y + 89);
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
            sellIndex3 = e.RowIndex;

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

            DataTable dt = m.CartMedicine(dataGridView2.Rows[index7].Cells[0].Value.ToString());
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

            //DataTable dt5 = ph.cartPharmacy(globalID);
            //dataGridView2.DataSource = dt5;

        }

        private void changePharmacyMed()
        {
            if(index7 > -1)
            {
                DataTable dt = m.CartMedicine(dataGridView2.Rows[index7].Cells[0].Value.ToString());
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

            DataTable dt = m.SearchMedicine(textBox22.Text, dataGridView2.Rows[index7].Cells[0].Value.ToString());
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
            index9 = e.RowIndex;
            changeFactoryMed();
        }

        private void dataGridView13_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index10 = e.RowIndex;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (index10 > -1 && dt13.Rows.Count != 0)
            {
                dt13.Rows[index10].Delete();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (dataGridView13.Rows.Count != 0)
            {
                BuyFactoryCheckout c = new BuyFactoryCheckout(dt13, dataGridView12.Rows[index9].Cells[0].Value.ToString(), globalID);
                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
                c.ShowDialog();

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
                    pictureBox6.Image = Image.FromFile(m.GetFactoryImagePath(dataGridView14.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView12.Rows[index9].Cells[0].Value.ToString()));
                }
                catch (System.IO.FileNotFoundException)
                {
                    pictureBox6.Image = Image.FromFile(@"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png");
                }

                pictureBox6.Location = new Point(rect.X + 353, rect.Y + 89);
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
            sellIndex4 = e.RowIndex;

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

            DataTable dt = m.CartFactory(dataGridView12.Rows[index9].Cells[0].Value.ToString());
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
            if(index9 > -1)
            {
                DataTable dt = m.CartFactory(dataGridView12.Rows[index9].Cells[0].Value.ToString());
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
            DataTable dt = m.SearchFactoryMedicine(textBox25.Text, dataGridView12.Rows[index9].Cells[0].Value.ToString());
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
            index11 = e.RowIndex;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView15.Rows[index11];
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
            DataGridViewRow selectedRow = dataGridView15.Rows[index11];
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
            ph.updatePharmacy(email.Text, password.Text, globalID);
            tabControl7.SelectedIndex = 2;
            tabControl7.SelectedIndex = 1;
            label74.ForeColor = Color.Green;
            label74.Text = "Updated Successfully";
        }
    }
}
