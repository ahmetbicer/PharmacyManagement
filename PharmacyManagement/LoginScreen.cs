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

        Employee emp = new Employee();
        Pharmacy p = new Pharmacy();
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = p.Login(textBox1.Text, textBox2.Text);
            if (r == 1)
            {
                this.Hide();
                var m = new MainScreen(textBox1.Text);
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
            int r = emp.Login(textBox3.Text, textBox4.Text);
            if (r == 1)
            {
                string pName = emp.getPharmacy(textBox3.Text);
                this.Hide();
                var m = new MainScreen(textBox3.Text, pName);
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

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var r = new Register();
            r.Closed += (s, args) => this.Close();
            r.StartPosition = FormStartPosition.Manual;
            r.Location = new Point(this.Location.X, this.Location.Y);
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this, new EventArgs());
            }
        }
    }
}