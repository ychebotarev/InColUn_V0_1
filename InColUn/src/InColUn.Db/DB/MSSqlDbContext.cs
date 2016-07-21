using System.Data;
using System.Data.SqlClient; 
using LoggerFacade;
using MetricsFacade;

namespace InColUn.Db
{
    public class MSSqlDbContext : BasicDbContext
    {
        //@"Server=YCHEBOTAREV2\SQLEXPRESS;Database=InColUn;User ID=UserTest;Password=1qaz2wsx;Connection Timeout=1;"
        public MSSqlDbContext(string connectionString, ILogger logger, IMetricsService metricService) 
            :base(connectionString, logger, metricService)
        {
        }

        public override IDbConnection GetDbConnection()
        {
            var connection = new SqlConnection(this.ConnectionString);
            return connection;
        }
    }
}
