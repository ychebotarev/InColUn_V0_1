using System;
using Dapper;

namespace InColUn.Db
{
    public class BasicTableService : ITableService
    {
        protected IDbContext _dbContext;

        public BasicTableService()
        {
        }

        public BasicTableService(MySqlDBContext dbContext)
        {
            this.SetContext(dbContext);
        }

        public void SetContext(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        protected bool ExecuteInsert(string query, object queryparams)
        {
            try
            {
                this._dbContext.GetDbConnection().Execute(query, queryparams);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
