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
    public class Logs
    {
        public void AddLogs(string log, string user)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("insert into logs_{0} (Logs) values(@Log);",user);         
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Log", log));
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public DataTable SeeLogs(string user)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select Logs from logs_{0} ORDER by LogsId DESC", user);
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
