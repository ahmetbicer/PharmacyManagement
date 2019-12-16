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
    public partial class BuyFactoryCheckout : Form
    {
        DataTable dt1;
        Medicine m = new Medicine();
        Sell s = new Sell();
        Buy b = new Buy();
        MedicineFactories mf = new MedicineFactories();
        Logs l = new Logs();

        string user;
        string globalid;
        public BuyFactoryCheckout(DataTable dt, string username, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            user = username;
            globalid = globalID;
        }

        private void BuyFactoryCheckout_Load(object sender, EventArgs e)
        {
            DataTable dt2 = mf.checkoutFactory(user);
            dataGridView2.DataSource = dt2;
            float price = 0;

            dataGridView1.DataSource = dt1;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                price += float.Parse(row.Cells[3].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
            }

            label5.Text = (price - ((price * 8) / 100)).ToString() + "$";

            label3.Text = ((price * 8) / 100).ToString() + "$";

            label4.Text = price.ToString() + "$";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                DataTable dt = m.GetUsageDetails(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), user);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                b.BuyMedicine(row.Cells[0].Value.ToString(), int.Parse(row.Cells[1].Value.ToString()), int.Parse(row.Cells[2].Value.ToString()), globalid);
                l.AddLogs(String.Format("{0}  -  '{1}' buy the medicine '{2}' from factory  '{3}'", DateTime.Now.ToString(), globalid, row.Cells[0].Value.ToString(), user), globalid);
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
