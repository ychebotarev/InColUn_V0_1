using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InColUn.Db
{
    public abstract class BasicDbContext : IDbContext
    {
        protected Dictionary<string, ITableService> _tableServices;

        public BasicDbContext()
        {
            this._tableServices = new Dictionary<string, ITableService>();
        }

        public abstract DbConnection GetDbConnection();

        public void AddTableService<T>(T service) where T : ITableService
        {
            var name = typeof(T).Name;
            this._tableServices[name] = service;
        }

        public T GetTableService<T>() where T : class, ITableService
        {
            ITableService service = null;

            var name = typeof(T).Name;
            this._tableServices.TryGetValue(name, out service);
            return service as T;
        }
    }
}