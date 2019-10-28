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
        public Login()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Username,Password FROM pharmacyList WHERE Username=@Username AND Password = @Password", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                            }
                            if (count == 1)
                            {
                                this.Hide();
                                var m = new MainScreen(textBox1.Text);
                                m.Closed += (s, args) => this.Close();
                                m.StartPosition = FormStartPosition.Manual;
                                m.Location = new Point(this.Location.X, this.Location.Y);
                                m.Show();
                            }
                            else if (count == 0)
                            {
                                MessageBox.Show("Username or password is wrong!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
