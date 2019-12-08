using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PharmacyManagement
{
    public class Sell
    {
        public void SellMedicine(string name,int stock,int quantity, string globalID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = String.Format("update medicines_{0} set Stock = Stock - @quantity where Name= @Name", globalID);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@Name", name));
            cmd.Parameters.Add(new SQLiteParameter("@quantity", quantity));
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
