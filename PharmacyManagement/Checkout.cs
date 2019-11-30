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
    public partial class Checkout : Form
    {
        public Checkout(object dt, string patID)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
            label1.Text = patID;
        }
    }
}
