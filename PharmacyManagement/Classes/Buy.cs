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
    public class Buy
    {
        public void BuyMedicine(string name, int stock, int quantity, string globalID)
        {
            int c1 = CheckMedCount(globalID);
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("insert into medicines_{0}(Name,Stock) Values(@Name,@quantity) on CONFLICT(Name) do update set Stock = Stock + @quantity",globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", name));
            cmd.Parameters.Add(new SQLiteParameter("@quantity", quantity));
            con.Open();
            cmd.ExecuteNonQuery();
            int c2 = CheckMedCount(globalID);
            if(c1 != c2)
            {
                InsertUsageInfo(globalID);
            }
        }

        public int CheckMedCount(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = String.Format("select count(*) from medicines_{0}", globalID);
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        int count = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void InsertUsageInfo(string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("insert into medicines_{0}_usage(Dose,Definition,Ingredients,Report) Values('fill','fill','fill','fill')", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
