using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using My_Portfolio.Interface;
using My_Portfolio.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml;

namespace My_Portfolio.DAO
{
    public class EmployeeDAO : IEmployees
    {
        private readonly string _ConnectionString;
        public EmployeeDAO(IOptions<ConnectionString> connectionString)
        {
            this._ConnectionString = connectionString.Value.SQLDatabase;
        }

        public List<Employees> GetAllEmployeeData()
        {
            List<Employees> employeesList = new List<Employees>();
            string Query = "SP_Get_EmployeeLog";
            using (SqlConnection con = new SqlConnection(this._ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employees employee = new Employees
                        {
                            EmployeeLog_Id = Convert.ToInt32(reader["EmployeeLog_Id"]),
                            Firstname = reader["FirstName"].ToString(),
                            Lastname = reader["LastName"].ToString(),
                            Username = reader["Username"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            DateOfBirth = (string)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Password = reader["Password"].ToString(),
                            ConfirmPassword = reader["ConfirmPassword"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Country = reader["Country"].ToString(),
                            Zipcode = Convert.ToInt32(reader["Zipcode"])
                        };
                        employeesList.Add(employee);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching employee data", ex);
                }
                finally
                {
                    con.Close();
                }
                return employeesList;
            }


        }

        public int SaveEmployeeData(Employees employees)
        {
            int rowAffected = 0;
            string Query = "SP_Add_EmployeeLog";
            using (SqlConnection con = new SqlConnection(this._ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Firstname", employees.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", employees.Lastname);
                    cmd.Parameters.AddWithValue("@Username", employees.Username);
                    cmd.Parameters.AddWithValue("@Gender", employees.Gender);
                    cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(employees.Age));
                    cmd.Parameters.AddWithValue("@DateOfBirth", employees.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", employees.Email);
                    cmd.Parameters.AddWithValue("@Phone", employees.Phone);
                    cmd.Parameters.AddWithValue("@Password", employees.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", employees.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@Address", employees.Address);
                    cmd.Parameters.AddWithValue("@City", employees.City);
                    cmd.Parameters.AddWithValue("@State", employees.State);
                    cmd.Parameters.AddWithValue("@Country", employees.Country);
                    cmd.Parameters.AddWithValue("@Zipcode", Convert.ToInt32(employees.Zipcode));
                    con.Open();
                    rowAffected = cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return rowAffected;
        }

        public int UpdateEmployeeData(Employees employees)
        {
            int rowAffected = 0;
            string Query = "SP_Update_EmployeeLog";
            using (SqlConnection con = new SqlConnection(this._ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeLog_Id", Convert.ToInt32(employees.EmployeeLog_Id));
                    cmd.Parameters.AddWithValue("@Firstname", employees.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", employees.Lastname);
                    cmd.Parameters.AddWithValue("@Username", employees.Username);
                    cmd.Parameters.AddWithValue("@Gender", employees.Gender);
                    cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(employees.Age));
                    cmd.Parameters.AddWithValue("@DateOfBirth", employees.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", employees.Email);
                    cmd.Parameters.AddWithValue("@Phone", employees.Phone);
                    cmd.Parameters.AddWithValue("@Password", employees.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", employees.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@Address", employees.Address);
                    cmd.Parameters.AddWithValue("@City", employees.City);
                    cmd.Parameters.AddWithValue("@State", employees.State);
                    cmd.Parameters.AddWithValue("@Country", employees.Country);
                    cmd.Parameters.AddWithValue("@Zipcode", Convert.ToInt32(employees.Zipcode));
                    con.Open();
                    rowAffected = cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return rowAffected;
        }

        public Employees GetEmployeeById(int Emp_Id)
        {
            Employees employee = new Employees();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                string query = "select*from EmployeeLog WHERE EmployeeLog_Id = @EmployeeLog_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeLog_Id", Emp_Id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee.EmployeeLog_Id = Convert.ToInt32(reader["EmployeeLog_Id"]);
                    employee.Firstname = reader["FirstName"].ToString();
                    employee.Lastname = reader["LastName"].ToString();
                    employee.Username = reader["Username"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.Age = Convert.ToInt32(reader["Age"]);
                    employee.DateOfBirth = (string)reader["DateOfBirth"];
                    employee.Email = reader["Email"].ToString();
                    employee.Phone = reader["Phone"].ToString();
                    employee.Password = reader["Password"].ToString();
                    employee.ConfirmPassword = reader["ConfirmPassword"].ToString();
                    employee.Address = reader["Address"].ToString();
                    employee.City = reader["City"].ToString();
                    employee.State = reader["State"].ToString();
                    employee.Country = reader["Country"].ToString();
                    employee.Zipcode = Convert.ToInt32(reader["Zipcode"]);
                }
                return employee;
            }
        }


        public int DeleteEmployee(int Emp_Id)
        {
            int retVal = 0;
            string Query = "SP_Delete_EmployeeLog";
            using (SqlConnection con = new SqlConnection(this._ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeLog_Id", Convert.ToInt32(Emp_Id));
                con.Open();
                retVal = cmd.ExecuteNonQuery();


            }
            return retVal;
        }



        public Employees UserLogin(Employees employees)
        {
            Employees loggedInEmployee = null;

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM EmployeeLog WHERE Username = @Username AND Password = @Password;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", employees.Username);
                cmd.Parameters.AddWithValue("@Password", employees.Password);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        loggedInEmployee = new Employees
                        {
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                          
                        };
                    }
                }
                con.Close();
            }

            return loggedInEmployee;
        }


    }
}
