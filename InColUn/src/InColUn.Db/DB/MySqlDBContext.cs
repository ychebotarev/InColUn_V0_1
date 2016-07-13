using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace InColUn.Db
{
    public class MySqlDBContext : BasicDbContext
    {
        //static readonly string connStr = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
        public MySqlDBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public override DbConnection GetDbConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }
    }
}