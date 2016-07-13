using System;
using Helpers;
using InColUn.Db.Models;
using Dapper;

namespace InColUn.Db
{
    public class UserTableService : BasicTableService
    {
        public UserTableService(MySqlDBContext dbContext):base(dbContext)
        {
        }

        /// <summary>
        /// Search for the user by user id.
        /// </summary>
        /// <param name="Id">User id</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        public User FindUserById(long Id)
        {
            var connection = this._dbContext.GetDbConnection();
            var query = string.Format("select * from users where id = {0}", Id);
            var user = connection.QuerySingleOrDefault<User>(query);

            return user;
        }

        /// <summary>
        /// Search for the user by user login string.
        /// </summary>
        /// <param name="loginString">User login string. Usually - email for local users, 
        /// "Profile Id" for external logins</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        public User FindUserByLoginString(string loginString)
        {
            var connection = this._dbContext.GetDbConnection();
            //var query = string.Format("select * from users where login_string = '{0}'", loginString);
            var user = connection.QuerySingleOrDefault<User>
                ("select * from users where login_string = @login_string", new { login_string  = loginString });

            return user;
        }

        /// <summary>
        /// Delete user from a database by Id
        /// </summary>
        /// <param name="Id">Unique user Id</param>
        public void DeleteUserById(long Id)
        {
            var deleteQuery = string.Format("DELETE FROM users WHERE id = {0}",Id);
            this._dbContext.GetDbConnection().Execute(deleteQuery);
        }

        /// <summary>
        /// Create local user
        /// Email will be used as indexed login_string
        /// </summary>
        /// <param name="Id">User id. Should be unique</param>
        /// <param name="name">User name. Will be displayed in UX</param>
        /// <param name="password">User password. Will be converted into hash</param>
        /// <param name="email">Uer email</param>
        /// <returns></returns>
        public bool CreateLocalUser(long Id, string name, string password, string email)
        {
            var hs = Crypto.GeneratePasswordHashSalt(password);
            var insertQuery = "INSERT INTO users (id, login_string, password_hash, salt, display_name, email, auth_provider, created, status)" +
                " VALUES (@id,@ls, @ps,@salt, @name, @email,'L',NOW(),'N')";

            return this.ExecuteInsert(insertQuery, new
            {
                id = Id,
                ls = email,
                ps = hs.Item1,
                salt = hs.Item2,
                name = name,
                email = email
            });
        }

        /// <summary>
        /// Create user from external auth proviced
        /// </summary>
        /// <param name="Id">Unique user id.</param>
        /// <param name="loginString">User unique login string. Useally in form of "id" from external auth provider.</param>
        /// <param name="displayName">User Display name if available. Can be null.</param>
        /// <param name="email">User email if available. Can be null/empty.</param>
        /// <param name="externalProvider">External provider Id. 'G' - Google, ' F' - Facebook</param>
        /// <returns></returns>
        public bool CreateExternalUser(long Id, 
            string loginString, 
            string displayName, 
            string email, 
            string externalProvider)
        {
            var insertQuery = "INSERT INTO users (id, login_string, display_name, email, auth_provider, created, status)" +
                " VALUES (@id, @ls, @name, @email, @provider, NOW(),'N')";

            return this.ExecuteInsert(insertQuery, new
            {
                id = Id,
                ls = loginString,
                name = displayName,
                email = email,
                provider = externalProvider
            });
        }
    }
}
