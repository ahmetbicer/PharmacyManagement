using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace PharmacyManagement
{
    public class Pharmacy
    {
        public void AddPharmacy(string username,string email,string password)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query1 = String.Format("insert into pharmacyList (Username,Email,Password) values(@Username,@Email,@Password);");
            string query2 = String.Format("CREATE TABLE medicines_{0}('MedId' INTEGER,'Name'  TEXT, 'Stock' TEXT,'Price'    TEXT,'ImgPath'    TEXT,PRIMARY KEY('MedId'));", username);
            string query3 = String.Format("CREATE TABLE medicines_{0}_usage('MedId' INTEGER,'Dose'  TEXT, 'Definition' TEXT,'Ingredients'    TEXT,'Report' TEXT,PRIMARY KEY('MedId'));", username);
            string query4 = String.Format("CREATE TABLE patients_{0} ('PatId' INTEGER,'ID'    INTEGER,'Name'  TEXT,'Surname'   TEXT,'Age'   INTEGER,'City'  TEXT,'HaveReport'    INTEGER,PRIMARY KEY('PatId')); ", username);
            string query = query1 + query2 + query3 + query4;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            cmd.Parameters.Add(new SQLiteParameter("@Email", email));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password));
            con.Open();
            cmd.ExecuteNonQuery();
            
        }

        public int Login(string username, string password)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Username,Password FROM pharmacyList WHERE Username=@Username AND Password = @Password", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                                
                            }
                            if (count == 1)
                            {
                                return 1;
                            }
                            else if (count == 0)
                            {
                                return 0;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }


        public DataTable cartPharmacy(string username)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = "select Username from pharmacyList where Username != @Username";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable searchPharmacy(string username)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = "select Username from pharmacyList where Username like @Username";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Username", username + "%"));
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public DataTable checkoutPharmacy(string username)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select Username from pharmacyList where Username = '{0}'", username);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
