using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace InColUn.Db
{
    public class MySqlDBContext : IDbContext
    {
        //static readonly string connStr = "server = localhost; user = root; database = incolun; port = 3306; password = !qAzXsW2";
        private Dictionary<string, ITableService> _tableServices;
        public MySqlDBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            this._tableServices = new Dictionary<string, ITableService>();
        }

        public string ConnectionString { get; private set; }

        public DbConnection GetDbConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        public void AddTableService<T>(T service) where T: ITableService
        {
            this._tableServices[nameof(T)] = service;
        }

        public T GetTableService<T>() where T : class, ITableService
        {
            ITableService service = null;
            this._tableServices.TryGetValue(nameof(T), out service);
            return service as T;
        }
    }
}
