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
    public class Employee
    {
        public int Login(string username, string password)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Username,Password FROM employeeList WHERE Username=@Username AND Password = @Password", conn))
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
        
        public string getPharmacy(string empName)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = "select PharmacyName from employeeList where Username = @Username";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", empName);
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(dt);
                        string count = dt.Rows[0].ItemArray[0].ToString();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void AddEmployee(string id,string name, string age, string gender, string salary, string username, string password, string pharmacyname)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = "insert into employeeList (EmployeeID,NameSurname,Age,Gender,Salary,Username,Password,PharmacyName) values(@EmployeeID,@NameSurname,@Age,@Gender,@Salary,@Username,@Password,@PharmacyName);";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@EmployeeID", id));
            cmd.Parameters.Add(new SQLiteParameter("@NameSurname", name));
            cmd.Parameters.Add(new SQLiteParameter("@Age", age));
            cmd.Parameters.Add(new SQLiteParameter("@Gender", gender));
            cmd.Parameters.Add(new SQLiteParameter("@Salary", salary));
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password));
            cmd.Parameters.Add(new SQLiteParameter("@PharmacyName", pharmacyname));
            con.Open();
            cmd.ExecuteNonQuery();

        }

        public DataTable viewEmployees(string globalID)
        {
            try
            {
                using (var conn = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db"))
                {
                    conn.Open();
                    string query = "select EmployeeID,NameSurname,Age,Gender,Salary,Username from employeeList where PharmacyName = @PharmacyName";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@PharmacyName", globalID));
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

        public void UpdateEmployee(string id, string name, string age, string gender, string salary, string username, string password)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = "update employeeList set NameSurname = @NameSurname, Age = @Age, Gender = @Gender, Salary = @Salary, Username = @Username, Password = @Password where EmployeeID = @EmployeeID";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@EmployeeID", id));
            cmd.Parameters.Add(new SQLiteParameter("@NameSurname", name));
            cmd.Parameters.Add(new SQLiteParameter("@Age", age));
            cmd.Parameters.Add(new SQLiteParameter("@Gender", gender));
            cmd.Parameters.Add(new SQLiteParameter("@Salary", salary));
            cmd.Parameters.Add(new SQLiteParameter("@Username", username));
            cmd.Parameters.Add(new SQLiteParameter("@Password", password));
            con.Open();
            cmd.ExecuteNonQuery();

        }

        public void deleteEmployee(string EmployeeID)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source = C:\Users\ahmtb\Desktop\pdb\pharmacy.db");
            string query = "delete from employeeList where EmployeeID = @EmployeeID";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.Add(new SQLiteParameter("@EmployeeID", EmployeeID));
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
