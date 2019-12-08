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
    public partial class BuyPharmacyCheckout : Form
    {
        DataTable dt1;
        Medicine m = new Medicine();
        Buy b = new Buy();
        Pharmacy ph = new Pharmacy();
        string user;
        string globalid;
        public BuyPharmacyCheckout(DataTable dt, string username, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            user = username;
            globalid = globalID;
        }

        private void BuyPharmacyCheckout_Load(object sender, EventArgs e)
        {
            DataTable dt2 = ph.checkoutPharmacy(user);
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
                DataTable dt = m.GetUsageDetails(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),user);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                b.BuyMedicine(row.Cells[0].Value.ToString(), int.Parse(row.Cells[1].Value.ToString()), int.Parse(row.Cells[2].Value.ToString()), globalid);
            }
            this.Close();
        }
    }
}
