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
    public partial class LoginAsEmployee : Form
    {
        Employee emp = new Employee();
        public LoginAsEmployee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = emp.Login(textBox1.Text, textBox2.Text);
            if (r == 1)
            {
                string pName = emp.getPharmacy(textBox1.Text);
                this.Hide();
                var m = new MainScreen(textBox1.Text,pName);
                m.Closed += (s, args) => this.Close();
                m.StartPosition = FormStartPosition.Manual;
                m.Location = new Point(this.Location.X, this.Location.Y);
                m.Show();
            }
            if (r == 0)
            {
                MessageBox.Show("Username or password is wrong!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var l = new LoginScreen();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }
    }
}
