using System;
using InColUn.Models;
using Helpers;
using Dapper;

namespace InColUn.DB
{
    public class UserTable
    {
        private MySqlDBContext _dbContext;
        public UserTable(MySqlDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        User FindUserById(long Id)
        {
            var user = this._dbContext.SqlConnection()
                .QuerySingleOrDefault<User>("select * from users where Id = @0", Id);

            return user;
        }

        User FindUserByLoginString(string loginString)
        {
            var user = this._dbContext.SqlConnection()
                .QuerySingleOrDefault<User>("select * from users where login_string = @0", loginString);

            return user;
        }

        void CreateLocalUser(long Id, string name, string password, string email)
        {
            var hs = Crypto.GeneratePasswordHashSalt(password);
            var insertQuery = string.Format("INSERT INTO users (Id, login_string, password_hash, salt, display_name, email, type, created, status)" +
                " VALUES ('{0}','{1}', '{2}',    '{3}',    '{4}', '{5}','L',NOW(),'N')",
                           Id,   email, hs.Item1, hs.Item2, name, email);
            var user = this._dbContext.SqlConnection().Execute(insertQuery);
        }

        void CreateExternalUser(long Id, string loginString, string displayName, string email, string externalProvider)
        {
            var insertQuery = string.Format("INSERT INTO users (Id, login_string, display_name, email, type, created, status)" +
                " VALUES ('{0}','{1}',        '{2}',      '{3}', '{4}', NOW(),'N')",
                           Id,   loginString, displayName, email, externalProvider);
            var user = this._dbContext.SqlConnection().Execute(insertQuery);
        }
    }
}