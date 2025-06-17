using System.Data.SqlClient;
using TodoTest.Models;


namespace TodoTest.Services
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddTodoItem(TodoItem item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO TodoItems (Text) OUTPUT INSERTED.Id VALUES (@Text)", conn);
                cmd.Parameters.AddWithValue("@Text", item.Text);
                return (int)cmd.ExecuteScalar(); 
            }
        }


        public void DeleteTodoItem(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM TodoItems WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
