﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace PharmacyManagement
{
    public class Medicine
    {
        public void AddMedicine(string name,string stock,string price,string dose,string report,string definition, string ingredients, string imgpath,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query1 = String.Format("insert into medicines_{0} (Name,Stock,Price,ImgPath) values('{1}','{2}','{3}',@ImgPath); ", globalID,name,stock,price);
            string query2 = String.Format("insert into medicines_{0}_usage (Dose,Definition,Ingredients,Report) values('{1}','{2}','{3}','{4}'); ", globalID,dose,definition,ingredients,report);
            string query = query1 + query2;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@ImgPath", imgpath));
            con.Open();
            cmd.ExecuteNonQuery();
            
        }

        public DataTable ViewMedicine(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT * FROM medicines_{0}", globalID);
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

        public DataTable CartMedicine(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT Name,Stock,Price FROM medicines_{0}", globalID);
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

        public void DeleteMedicine(string medName,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("delete from medicines_{0} where Name = @Name", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateMedicine(string medId, string medName, string medStock, string medReport, string medUsage,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("update medicines_{0} set Name = @Name, Stock = @Stock, Report = @Report where MedId = @MedId", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@MedId", medId));
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", medStock));
            cmd.Parameters.Add(new SQLiteParameter("@Report", medReport));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public DataTable SearchMedicine(string name, string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT Name,Stock,Price FROM medicines_{0} where Name like @Name", globalID);                   
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name", name + "%"));
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

        public DataTable GetUsageDetails(string name, string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select medicines_{0}_usage.Dose,medicines_{0}_usage.Definition,medicines_{0}_usage.Ingredients, medicines_{0}_usage.Report  from medicines_{0} inner join medicines_{0}_usage on medicines_{0}.MedId = medicines_{0}_usage.MedId where Name = @Name", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name", name));
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

        public string GetImagePath(string name,string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select ImgPath from medicines_{0} where Name = @Name", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name", name));
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        string imgpath = dt.Rows[0].Field<string>(0);
                        if(imgpath == null)
                        {
                            string eximg = @"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Assets\Images\defaultimage.png";
                            return eximg;
                        }
                        else
                        {
                            return imgpath;
                        }
                        
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
