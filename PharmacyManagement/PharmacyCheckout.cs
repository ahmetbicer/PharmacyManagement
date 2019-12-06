﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagement
{
    public partial class PharmacyCheckout : Form
    {
        DataTable dt1;
        Medicine m = new Medicine();
        Sell s = new Sell();
        Pharmacy ph = new Pharmacy();
        string user;
        string globalid;
        public PharmacyCheckout(DataTable dt, string username, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            user = username;
            globalid = globalID;
        }

        private void PharmacyCheckout_Load(object sender, EventArgs e)
        {
            DataTable dt2 = ph.checkoutPharmacy(user);
            dataGridView2.DataSource = dt2;
            float price = 0;

            dataGridView1.DataSource = dt1;
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Details";
            col.Text = "Details";
            col.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(col);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                price += float.Parse(row.Cells[3].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
            }

            label5.Text = (price - ((price * 8) / 100)).ToString() + "$";

            label3.Text = ((price * 8) / 100).ToString() + "$";

            label4.Text = price.ToString() + "$";

            label8.Text = "Push details button to see...";
            label10.Text = "Push details button to see...";
            label12.Text = "Push details button to see...";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                DataTable dt = m.GetUsageDetails(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), globalid);

                label8.Text = dt.Rows[0].Field<string>(0);
                label10.Text = dt.Rows[0].Field<string>(2);
                if(dt.Rows[0].Field<string>(3) == "0")
                {
                    label12.Text = "Doesn't need doctor report to sell.";
                }
                else if (dt.Rows[0].Field<string>(3) == "1")
                {
                    label12.Text = "Need doctor report to sell.";
                }
                richTextBox1.Text = dt.Rows[0].Field<string>(1);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                s.SellMedicine(row.Cells[0].Value.ToString(), int.Parse(row.Cells[1].Value.ToString()), int.Parse(row.Cells[2].Value.ToString()), globalid);
            }
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}