using System.Data.Common;
using LoggerFacade;
using MetricsFacade;
 
namespace InColUn.Db
{
    public interface IDbContext
    {
        DbConnection GetDbConnection();
        DbConnection DbConnection { get; }
        ILogger Logger { get; }
        IMetricsService Metrics { get; }

        void AddTableService<T>(T service) where T : ITableService;
        T GetTableService<T>() where T : class, ITableService;
    }
}
