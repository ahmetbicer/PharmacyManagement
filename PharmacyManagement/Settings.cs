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
    public partial class Settings : Form
    {
        bool clicked = false;

        public Settings()
        {
            InitializeComponent();
            pictureBox1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                pictureBox1.Show();
                clicked = true;
            }
            else if (clicked == true)
            {
                pictureBox1.Hide();
                clicked = false;
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Show();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Hide();
        }
    }
}
