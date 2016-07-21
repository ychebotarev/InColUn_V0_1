using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

using LoggerFacade;
using MetricsFacade;

namespace InColUn.Db
{
    public class MySqlDBContext : BasicDbContext
    {
        //static readonly string connStr = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
        public MySqlDBContext(string connectionString, ILogger logger, IMetricsService metricService) 
            :base(connectionString, logger, metricService)
        {
        }

        public override IDbConnection GetDbConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }
    }
}