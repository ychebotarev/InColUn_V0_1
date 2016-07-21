using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using LoggerFacade;
using MetricsFacade;

namespace InColUn.Db
{
    public abstract class BasicDbContext : IDbContext
    {
        protected Dictionary<string, ITableService> tableServices;
        protected ILogger logger;
        protected IMetricsService metricService;

        public BasicDbContext(string connectionString, ILogger logger, IMetricsService metricService)
        {
            this.logger = logger;
            this.metricService = metricService;
            this.ConnectionString = connectionString;
            this.tableServices = new Dictionary<string, ITableService>();
        }

        public string ConnectionString { get; private set; }

        public abstract IDbConnection GetDbConnection();

        public ILogger Logger => this.logger;
        public IMetricsService Metrics => this.metricService;

        public IDbConnection DbConnection => this.GetDbConnection();

        public void AddTableService<T>(T service) where T : ITableService
        {
            var name = typeof(T).Name;
            this.tableServices[name] = service;
        }

        public T GetTableService<T>() where T : class, ITableService
        {
            ITableService service = null;

            var name = typeof(T).Name;
            this.tableServices.TryGetValue(name, out service);
            return service as T;
        }
    }
}