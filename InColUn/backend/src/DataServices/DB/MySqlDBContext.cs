using System.Data;
using MySql.Data.MySqlClient;

namespace InColUn.DB
{
    public class MySqlDBContext : IDbContext
    {

        //static readonly string connStr = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
        public MySqlDBContext(string connectionString) 
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public IDbConnection DbConnection
        {
            get
            {
                return new MySqlConnection(this.ConnectionString);
            }
        }
    }
}