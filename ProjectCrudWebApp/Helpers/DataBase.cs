using System.Data.SqlClient;

namespace ProjectCrudWebApp.Helpers
{

    public class DataBase
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=ENWIN-548\\SQLExpress;Initial Catalog=ProjectDB;Integrated Security=True;";

            return new SqlConnection(connectionString);
        }
    }
}
