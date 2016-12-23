using System.Data;
using System.Data.SqlClient; 

namespace InColUn.DB
{
    public class MSSqlDbContext : IDbContext
    {
        //@"Server=YCHEBOTAREV2\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=1;"
        public MSSqlDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public IDbConnection DbConnection
        {
            get
            {
                return new SqlConnection(this.ConnectionString);
            }
        }
    }
}
