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
        public void AddMedicine(string name,string stock,string report,string usage)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"insert into medicine (Name,Stock,Report,Usage) values(@Name,@Stock,@Report,@Usage) ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", name));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", stock));
            cmd.Parameters.Add(new SQLiteParameter("@Report", report));
            cmd.Parameters.Add(new SQLiteParameter("@Usage", usage));
            con.Open();
            cmd.ExecuteNonQuery();
            
        }

        public DataTable ViewMedicine()
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM medicine", conn))
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

        public void DeleteMedicine(string medName)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"delete from medicine where Name = @Name";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateMedicine(string medId, string medName, string medStock, string medReport, string medUsage)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = @"update medicine set Name = @Name, Stock = @Stock, Report = @Report, Usage = @Usage where MedId = @MedId";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@MedId", medId));
            cmd.Parameters.Add(new SQLiteParameter("@Name", medName));
            cmd.Parameters.Add(new SQLiteParameter("@Stock", medStock));
            cmd.Parameters.Add(new SQLiteParameter("@Report", medReport));
            cmd.Parameters.Add(new SQLiteParameter("@Usage", medUsage));
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
