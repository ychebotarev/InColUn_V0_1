using System;
using Dapper;
using LoggerFacade;

namespace InColUn.Db
{
    public class BasicTableService : ITableService
    {
        protected IDbContext dbContext;

        public BasicTableService()
        {
        }

        public BasicTableService(IDbContext dbContext)
        {
            this.SetContext(dbContext);
        }

        public void SetContext(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Execute arbitrary query. Be aware
        /// </summary>
        /// <param name="query">Query to execute</param>
        public void Execute(string query)
        {
            this.dbContext.GetDbConnection().Execute(query);
        }

        protected bool ExecuteInsert(string query, object queryparams)
        {
            try
            {
                this.dbContext.GetDbConnection().Execute(query, queryparams);
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }

                return false;
            }
            return true;
        }

        protected bool ExecuteUpdate(string query, object queryparams)
        {
            try
            {
                this.dbContext.GetDbConnection().Execute(query, queryparams);
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }

                return false;
            }
            return true;
        }
    }
}
