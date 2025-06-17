using System;
using System.Data.SqlClient;

namespace TodoTest.Services
{
    public class ErrorLogger
    {
        private readonly string _connectionString;

        public ErrorLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Log(Exception ex)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO ErrorLog (Body, Time, Source)
                                     VALUES (@Body, @Time, @Source)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Body", ex.ToString());
                        command.Parameters.AddWithValue("@Time", DateTime.Now);
                        command.Parameters.AddWithValue("@Source", ex.Source ?? (object)DBNull.Value);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception logEx)
            {
                Console.WriteLine("Logging failed: " + logEx.Message);
            }
        }
    }
}
