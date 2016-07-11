using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InColUn.DB
{
    public class MySqlDBContext
    {
        //static readonly string connStr = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";

        public MySqlDBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public MySqlConnection SqlConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }
    }
}
