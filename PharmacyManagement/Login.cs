using System;
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
    public partial class Login : Form
    {
        Pharmacy p = new Pharmacy();
        public Login()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            int r = p.Login(textBox1.Text,textBox2.Text);
            if(r == 1)
            {
                this.Hide();
                var m = new MainScreen(textBox1.Text);
                m.Closed += (s, args) => this.Close();
                m.StartPosition = FormStartPosition.Manual;
                m.Location = new Point(this.Location.X, this.Location.Y);
                m.Show();
            }
            if(r == 0)
            {
                MessageBox.Show("Username or password is wrong!");
            }
            

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var r = new Register();
            r.Closed += (s, args) => this.Close();
            r.StartPosition = FormStartPosition.Manual;
            r.Location = new Point(this.Location.X,this.Location.Y);
            r.Show();


        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }

    }
}
