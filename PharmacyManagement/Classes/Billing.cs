using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PharmacyManagement
{
    public class Billing
    {
        public DataTable getGraphData(string year, string month)
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
                {
                    conn.Open();
                    string query = String.Format("select sum(Income), sum(Expense) from incomeandexpense_ahmet where Year = @Year and Month = @Month");
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Year", year));
                        cmd.Parameters.Add(new SQLiteParameter("@Month", month));
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

        public void AddIncome(string year,string month,string income,string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.DbPath);
            string query = String.Format("insert into incomeandexpense_{0} (Year,Month,Income) values('{1}','{2}','{3}'); ", globalID,year,month,income);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddExpense(string year, string month, string expense, string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.DbPath);
            string query = String.Format("insert into incomeandexpense_{0} (Year,Month,Expense) values('{1}','{2}','{3}'); ", globalID, year, month, expense);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
