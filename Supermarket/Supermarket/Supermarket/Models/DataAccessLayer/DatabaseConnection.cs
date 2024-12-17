using MySql.Data.MySqlClient;

namespace Supermarket.Models.DataAccessLayer
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString = "server=localhost;user=root;password=1q2w3e4r;database=supermarket";

        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
