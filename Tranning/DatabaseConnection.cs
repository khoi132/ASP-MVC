using Microsoft.Data.SqlClient;

namespace Tranning
{
    public class DatabaseConnection
    {
        public static string getStrConnection()
        {
            string sTrConnection = @"Data Source=Khoi\SQLEXPRESS03;Initial Catalog=Tranning;Integrated Security=True;TrustServerCertificate=True";
            return sTrConnection;
        }
        public static SqlConnection GetSqlConnection() 
        {
            string StrConnection = DatabaseConnection.getStrConnection();
            SqlConnection connection = new SqlConnection( StrConnection );
            return connection;
        }
    }
}
