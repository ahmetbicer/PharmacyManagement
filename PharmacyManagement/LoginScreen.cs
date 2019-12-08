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
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var l = new LoginAsEmployee();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var r = new Register();
            r.Closed += (s, args) => this.Close();
            r.StartPosition = FormStartPosition.Manual;
            r.Location = new Point(this.Location.X, this.Location.Y);
            r.Show();
        }
    }
}
