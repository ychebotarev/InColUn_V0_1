using System.Data;
using LoggerFacade;
using MetricsFacade;
 
namespace InColUn.Db
{
    public interface IDbContext
    {
        IDbConnection GetDbConnection();
        IDbConnection DbConnection { get; }
        ILogger Logger { get; }
        IMetricsService Metrics { get; }

        void AddTableService<T>(T service) where T : ITableService;
        T GetTableService<T>() where T : class, ITableService;
    }
}
