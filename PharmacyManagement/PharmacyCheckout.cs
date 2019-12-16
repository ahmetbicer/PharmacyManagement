using System;
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
        Logs l = new Logs();

        string user;
        string globalid;
        string empName;

        bool isEmployee = false;
        public PharmacyCheckout(DataTable dt, string username, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            user = username;
            globalid = globalID;
        }

        public PharmacyCheckout(DataTable dt, string username, string empUsername, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            user = username;
            globalid = globalID;
            empName = empUsername;
            isEmployee = true;
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
                if (isEmployee)
                {
                    l.AddLogs(String.Format("{0}  -  '{1}' sell the medicine '{2}' to pharmacy  '{3}'", DateTime.Now.ToString(), empName, row.Cells[0].Value.ToString(), user), globalid);
                }
                else
                {
                    l.AddLogs(String.Format("{0}  -  '{1}' sell the medicine '{2}' to pharmacy  '{3}'", DateTime.Now.ToString(), globalid, row.Cells[0].Value.ToString(), user), globalid);
                }
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
