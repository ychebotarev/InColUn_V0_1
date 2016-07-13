using System.Collections.Generic;
using System.Data.Common;

using LoggerFacade;

namespace InColUn.Db
{
    public abstract class BasicDbContext : IDbContext
    {
        protected Dictionary<string, ITableService> tableServices;
        protected ILogger logger;

        public BasicDbContext(string connectionString, ILogger logger)
        {
            this.logger = logger;
            this.ConnectionString = connectionString;
            this.tableServices = new Dictionary<string, ITableService>();
        }

        public string ConnectionString { get; private set; }

        public abstract DbConnection GetDbConnection();

        public ILogger Logger => this.logger;
        public DbConnection DbConnection => this.GetDbConnection();

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