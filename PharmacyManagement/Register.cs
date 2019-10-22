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
namespace PharmacyManagement
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            var l = new Login();
            l.Closed += (s, args) => this.Close();
            l.StartPosition = FormStartPosition.Manual;
            l.Location = new Point(this.Location.X, this.Location.Y);
            l.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"insert into pharmacy (Name,Email,Password) values(@Name,@Email,@Password); 
                                create table Pharmacy_B( ID int );";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", name.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Email", email.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            name.Clear();
            email.Clear();
            password.Clear();
            approval.Text = "Registered Successfully";
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, new EventArgs());
            }
        }
    }

    }
