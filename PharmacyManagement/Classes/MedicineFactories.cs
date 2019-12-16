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
    public class MedicineFactories
    {
        public DataTable cartFactory()
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
                {
                    conn.Open();
                    string query = "select FactoryName from MedicineFactories";
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

        public DataTable searchFactory(string FactoryName)
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
                {
                    conn.Open();
                    string query = "select FactoryName from MedicineFactories where FactoryName like @FactoryName";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@Username", FactoryName + "%"));
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


        public DataTable checkoutFactory(string FactoryName)
        {
            try
            {
                using (var conn = new SQLiteConnection(Properties.Settings.Default.DbPath))
                {
                    conn.Open();
                    string query = String.Format("select FactoryName from MedicineFactories where FactoryName = '{0}'", FactoryName);
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
