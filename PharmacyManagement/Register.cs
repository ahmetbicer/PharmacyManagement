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
            string query1 = String.Format("insert into pharmacyList (Username,Email,Password) values(@Username,@Email,@Password);");
            string query2 = String.Format("CREATE TABLE medicines_{0}('MedId' INTEGER,'Name'  TEXT, 'Stock' TEXT,'Report'    INTEGER,'Usage' TEXT,PRIMARY KEY('MedId'));", username.Text);
            string query3 = String.Format("CREATE TABLE patients_{0} ('PatId' INTEGER,'ID'    INTEGER,'Name'  TEXT,'Surname'   TEXT,'Age'   INTEGER,'City'  TEXT,'HaveReport'    INTEGER,PRIMARY KEY('PatId')); ",username.Text);
            string query = query1 + query2 + query3;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Username", username.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Email", email.Text));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            username.Clear();
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
