using Microsoft.Extensions.Options;
using My_Portfolio.Models;
using System.Data.SqlClient;
using System.Data;
using My_Portfolio.Interface;

namespace My_Portfolio
{
    public class DataAccessLayer : IEmail
    {
        private readonly string _connectionString;

        public DataAccessLayer(IOptions<ConnectionString> connectionString)
        {
            this._connectionString = connectionString.Value.SQLDatabase;
        }

        public int SaveEmailData(EmailModel emailModel)
        {
            int rowsAffected = 0;
            string query = "SP_Add_EmailLog";
            using (SqlConnection con = new SqlConnection(this._connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@E_Name", emailModel.E_Name);
                        cmd.Parameters.AddWithValue("@E_Email", emailModel.E_Email);
                        cmd.Parameters.AddWithValue("@E_Phone", emailModel.E_Phone);
                        cmd.Parameters.AddWithValue("@E_Message", emailModel.E_Message);
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Log or handle SQL exceptions
                    throw new Exception("An error occurred while saving email data.", ex);
                }
                finally
                {
                    // Ensures the connection is closed
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                return rowsAffected;
            }
        }

        void IEmail.SaveEmailData(EmailModel emailModel)
        {
            throw new NotImplementedException();
        }
    }
}
