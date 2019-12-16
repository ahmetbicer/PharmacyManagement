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
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.DbPath);
            string query1 = String.Format("insert into pharmacyList (Username,Email,Password) values(@Username,@Email,@Password);");
            string query2 = String.Format("CREATE TABLE medicines_{0}('MedId' INTEGER,'Name'  TEXT UNIQUE, 'Stock' TEXT,'Price'    TEXT DEFAULT '###','ImgPath'    TEXT,'Quantity'	TEXT DEFAULT 1,PRIMARY KEY('MedId'));", username);
            string query3 = String.Format("CREATE TABLE medicines_{0}_usage('MedId' INTEGER,'Dose'  TEXT, 'Definition' TEXT,'Ingredients'    TEXT,'Report' TEXT,PRIMARY KEY('MedId'));", username);
            string query4 = String.Format("CREATE TABLE logs_{0}('LogsId' INTEGER,'Logs'  TEXT,PRIMARY KEY('LogsId'));", username);
            string query = query1 + query2 + query3 + query4;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            cmd.Parameters.Add(new SQLiteParameter("@Email", email));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password));
            con.Open();
            cmd.ExecuteNonQuery();
            
        }

        public void DeletePharmacy(string username)
        {
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.DbPath);
            string query1 = String.Format("delete from pharmacyList where Username = @Username;");
            string query2 = String.Format("delete from employeeList where PharmacyName = @Username;");
            string query3 = String.Format("drop table medicines_{0};",username);
            string query4 = String.Format("drop table medicines_{0}_usage;", username);
            string query5 = String.Format("drop table logs_{0};", username);
            string query = query1 + query2 + query3 + query4 + query5;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public int Login(string username, string password)
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
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
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
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
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
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
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
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

        public DataTable getPharmacyInfo(string username)
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
                {
                    conn.Open();
                    string query = "select Email,Password from pharmacyList where Username = @Username";
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

        public void updatePharmacy(string email,string password,string username)
        {
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.DbPath);
            string query = "update pharmacyList set Email = @Email, Password = @Password where Username = @Username";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Email", email));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password));
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            con.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
