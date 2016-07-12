using System;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public class UserTable
    {
        private MySqlDBContext _dbContext;

        public UserTable(MySqlDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public User FindUserById(long Id)
        {
            var connection = this._dbContext.SqlConnection();
            var query = string.Format("select * from users where Id = {0}", Id);
            var user = connection.QuerySingleOrDefault<User>(query);

            return user;
        }

        public User FindUserByLoginString(string loginString)
        {
            var connection = this._dbContext.SqlConnection();
            var query = string.Format("select * from users where login_string = '{0}'", loginString);
            var user = connection.QuerySingleOrDefault<User>(query);

            return user;
        }

        public void DeleteUserById(long Id)
        {
            var deleteQuery = string.Format("DELETE FROM users WHERE Id = {0}",Id);
            this._dbContext.SqlConnection().Execute(deleteQuery);
        }

        public bool CreateLocalUser(long Id, string name, string password, string email)
        {
            var hs = Crypto.GeneratePasswordHashSalt(password);
            var insertQuery = string.Format("INSERT INTO users (Id, login_string, password_hash, salt, display_name, email, auth_provider, created, status)" +
                " VALUES ('{0}','{1}', '{2}',    '{3}',    '{4}', '{5}','L',NOW(),'N')",
                           Id, email, hs.Item1, hs.Item2, name, email);
            try
            {
                var result = this._dbContext.SqlConnection().Execute(insertQuery);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public bool CreateExternalUser(long Id, 
            string loginString, 
            string displayName, 
            string email, 
            string externalProvider)
        {
            var insertQuery = string.Format("INSERT INTO users (Id, login_string, display_name, email, auth_provider, created, status)" +
                " VALUES ('{0}','{1}',        '{2}',      '{3}', '{4}', NOW(),'N')",
                           Id, loginString, displayName, email, externalProvider);
            try
            {
                var result = this._dbContext.SqlConnection().Execute(insertQuery);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
