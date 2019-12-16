using System;
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
                    //string query = String.Format("SELECT * FROM medicines_{0}", globalID);
                    string query = String.Format("select medicines_{0}.Name,medicines_{0}.Stock,medicines_{0}.Price,medicines_{0}_usage.Dose,medicines_{0}_usage.Definition,medicines_{0}_usage.Ingredients, medicines_{0}_usage.Report  from medicines_{0} inner join medicines_{0}_usage on medicines_{0}.MedId = medicines_{0}_usage.MedId", globalID);
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
                    string query = String.Format("SELECT Name,Stock,Price FROM medicines_{0} where Price != '###'", globalID);
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

        public DataTable CartFactory(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT Name,Stock,Price FROM factory_{0}", globalID);
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

        public void DeleteMedicine(string medName,int id,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query1 = String.Format("delete from medicines_{0} where Name = @Name;", globalID);
            string query2 = String.Format("delete from medicines_{0}_usage where MedId = @MedId;", globalID);
            string query = query1 + query2;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            cmd.Parameters.Add(new SQLiteParameter("@MedId", id));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateMedicine(string medId, string medName, string medStock, string medPrice,string medDose,string medDefinition,string medIngredients,string medReport, string medImgPath, string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query1 = String.Format("update medicines_{0} set Name = @Name, Stock = @Stock, Price = @Price, ImgPath = @ImgPath where MedId = @MedId;", globalID);
            string query2 = String.Format("update medicines_{0}_usage set Dose = @Dose,Definition = @Definition,Ingredients = @Ingredients,Report = @Report where MedId = @MedId;", globalID);
            string query = query1 + query2;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@MedId", medId));
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", medStock));
            cmd.Parameters.Add(new SQLiteParameter("@Price", medPrice));
            cmd.Parameters.Add(new SQLiteParameter("@ImgPath", medImgPath));
            cmd.Parameters.Add(new SQLiteParameter("@Dose", medDose));
            cmd.Parameters.Add(new SQLiteParameter("@Definition", medDefinition));
            cmd.Parameters.Add(new SQLiteParameter("@Ingredients", medIngredients));
            cmd.Parameters.Add(new SQLiteParameter("@Report", medReport));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateMedicine(string medId, string medName, string medStock, string medPrice, string medDose, string medDefinition, string medIngredients, string medReport,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query1 = String.Format("update medicines_{0} set Name = @Name, Stock = @Stock, Price = @Price where MedId = @MedId;", globalID);
            string query2 = String.Format("update medicines_{0}_usage set Dose = @Dose,Definition = @Definition,Ingredients = @Ingredients,Report = @Report where MedId = @MedId;", globalID);
            string query = query1 + query2;
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@MedId", medId));
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", medStock));
            cmd.Parameters.Add(new SQLiteParameter("@Price", medPrice));
            cmd.Parameters.Add(new SQLiteParameter("@Dose", medDose));
            cmd.Parameters.Add(new SQLiteParameter("@Definition", medDefinition));
            cmd.Parameters.Add(new SQLiteParameter("@Ingredients", medIngredients));
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
                    string query = String.Format("SELECT Name,Stock,Price FROM medicines_{0} where Name like @Name and Price != '###'", globalID);  
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name","%" + name + "%"));
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

        public DataTable SearchFactoryMedicine(string name, string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT Name,Stock,Price FROM factory_{0} where Name like @Name", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name", "%" + name + "%"));
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

        public string GetFactoryImagePath(string name, string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select ImgPath from factory_{0} where Name = @Name", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Name", name));
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        string imgpath = dt.Rows[0].Field<string>(0);
                        if (imgpath == null)
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

        public DataTable ReadPrescription(string prescriptionID,string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select Name,Stock,Quantity,Price from medicines_{0} where MedId = (select med1 from prescription where prescriptionID = '{1}') or MedId = (select med2 from prescription where prescriptionID = '{1}') or MedId = (select med3 from prescription where prescriptionID = '{1}') or MedId = (select med4 from prescription where prescriptionID = '{1}');", globalID,prescriptionID);
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
                MessageBox.Show("Wrong Prescription Id!");
                return null;
            }
        }

        public string PrescriptionPatient(string prescriptionID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select ID from patients where patID = (select patID from prescription where prescriptionID = '{0}')", prescriptionID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        string id = dt.Rows[0].ItemArray[0].ToString();
                        return id;

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Wrong Patient Id!");
                return null;
            }
        }


    }
}
