using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace InColUn.DB
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Execute arbitrary query. Be aware
        /// </summary>
        /// <param name="query">Query to execute</param>
        public static bool Execute(this IDbContext dbContext, string query, object param = null)
        {
            try
            {   
                var connection = dbContext.DbConnection;
                connection.Execute(query, param);
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.LogException(string.Format("SQL exception for: {0}", query), e);
                return false;
            }
        }
        public static IEnumerable<T> Query<T>(this IDbContext dbContext, string query, object param = null)
        {
            try
            {
                var connection = dbContext.DbConnection;
                var result = connection.Query<T>(query, param);
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                Logger.Instance.LogException(string.Format("SQL exception for: {0}", query), e);
                return null;
            }
        }

        public static T QuerySingleOrDefault<T>(this IDbContext dbContext, string query, object param = null)
        {
            try
            {
                var connection = dbContext.DbConnection;
                var result = connection.QuerySingleOrDefault<T>(query, param);
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                Logger.Instance.LogException(string.Format("SQL exception for: {0}", query), e);
                return default(T);
            }
        }
    }
}
