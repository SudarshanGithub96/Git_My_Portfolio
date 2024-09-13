using My_Portfolio.Models;
using System.Data;
using System.Data.SqlClient;

namespace My_Portfolio
{
    public class EmployeeDataAccessLayer
    {
        string cs = ConnectionString.dbcs;

        public List<Employees> GetEmployees()
        {
            List<Employees> empList = new List<Employees>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from  Employee", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employees employees = new Employees();
                    employees.Employee_Id = Convert.ToInt32(reader["Employee_Id"]);
                    employees.Employee_Name = reader["Employee_Name"].ToString() ?? "";
                    employees.Gender = reader["Gender"].ToString() ?? "";
                    employees.Age = Convert.ToInt32(reader["Age"]);
                    employees.Designation = reader["Designation"].ToString() ?? "";
                    employees.City = reader["City"].ToString() ?? "";
                    empList.Add(employees);
                }
                return empList;
            }
        }

    }
}
