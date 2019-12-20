using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace PharmacyManagement
{
    public partial class ForgotPassword : Form
    {
        Pharmacy p = new Pharmacy();
        public ForgotPassword()
        {
            InitializeComponent();

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

        private void button3_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            if (reg.IsMatch(email.Text))
            {
                string pass = p.GetPassword(email.Text);
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    message.From = new MailAddress("noreplypharmacymanagement@gmail.com");
                    message.To.Add(new MailAddress(email.Text));
                    message.Subject = "Your password for Pharmacy Management System";
                    message.Body = "Password: " + pass;

                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("noreplypharmacymanagement@gmail.com", "***");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("err: " + ex.Message);
                }
                email.Clear();
                approval.ForeColor = Color.Green;
                approval.Text = "Sended successfully.";
            }
            else
            {
                approval.ForeColor = Color.Red;
                approval.Text = "Invalid email adress.";
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, new EventArgs());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {

        }
    }

    }
