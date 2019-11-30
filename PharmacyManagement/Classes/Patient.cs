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
    public class Patient
    {
        public void addPatient(string ID, string pat_name,string pat_surname,string pat_age,string pat_city,string pat_report,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("insert into patients_{0} (ID,Name,Surname,Age,City,HaveReport) values(@ID,@Name,@Surname,@Age,@City,@HaveReport) ", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@ID", ID));
            cmd.Parameters.Add(new SQLiteParameter("@Name", pat_name));
            cmd.Parameters.Add(new SQLiteParameter("@Surname", pat_surname));
            cmd.Parameters.Add(new SQLiteParameter("@Age", pat_age));
            cmd.Parameters.Add(new SQLiteParameter("@City", pat_city));
            cmd.Parameters.Add(new SQLiteParameter("@HaveReport", pat_report));
            con.Open();
            cmd.ExecuteNonQuery();
        }
        public DataTable viewPatients(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select * from patients_{0}", globalID);
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

        public DataTable cartPatients(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select ID,Name,Surname from patients_{0}", globalID);
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

        public void deletePatient(string patPatId, string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("delete from patients_{0} where PatId = @PatId", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@PatId",patPatId));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void updatePatient(string patPatId, string patID, string patName, string patSurname, string patAge, string patCity, string patHaveReport, string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("update patients_{0} set ID = @ID, Name = @Name, Surname = @Surname, Age = @Age, City = @City, HaveReport = @HaveReport where PatId = @PatId", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@PatId", patPatId));
            cmd.Parameters.Add(new SQLiteParameter("@ID", patID));
            cmd.Parameters.Add(new SQLiteParameter("@Name", patName));
            cmd.Parameters.Add(new SQLiteParameter("@Surname", patSurname));
            cmd.Parameters.Add(new SQLiteParameter("@Age", patAge));
            cmd.Parameters.Add(new SQLiteParameter("@City", patCity));
            cmd.Parameters.Add(new SQLiteParameter("@HaveReport", patHaveReport));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public DataTable SearchPatient(string ID, string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("SELECT ID,Name,Surname FROM patients_{0} where ID like @ID", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@ID", ID + "%"));
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
