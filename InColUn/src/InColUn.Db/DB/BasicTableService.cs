using System;
using System.Collections.Generic;
using Dapper;
using LoggerFacade;

namespace InColUn.Db
{
    public class BasicTableService : ITableService
    {
        private IDbContext dbContext;

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

        public IDbContext GetContext()
        {
            return this.dbContext;
        }

        /// <summary>
        /// Execute arbitrary query. Be aware
        /// </summary>
        /// <param name="query">Query to execute</param>
        public void Execute(string query)
        {
            try
            {
                var connection = this.dbContext.GetDbConnection();
                connection.Execute(query);
                connection.Close();
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }
            }
        }

        protected IEnumerable<T> Query<T>(string query)
        {
            try
            {
                var connection = this.dbContext.GetDbConnection();
                var result = connection.Query<T>(query);
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }
            }
            return null;
        }

        protected T QuerySingleOrDefault<T>(string query, object queryParams)
        {
            try
            {
                var connection = this.dbContext.GetDbConnection();
                var result = connection.QuerySingleOrDefault<T>(query, queryParams);
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }
            }
            return default(T);

        }

        protected T QuerySingleOrDefault<T>(string query)
        {
            try
            {
                var connection = this.dbContext.GetDbConnection();
                var result = connection.QuerySingleOrDefault<T>(query);
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                var logger = this.dbContext.Logger;
                if (logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("SQL exception for: {0}", query), e);
                    logger.Log(logEntry);
                }
            }
            return default(T);
        }

        protected bool ExecuteQuery(string query, object queryparams)
        {
            try
            {
                var connection = this.dbContext.GetDbConnection();
                connection.Execute(query, queryparams);
                connection.Close();
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
