using InColUn.Data.Models;
using InColUn.DB;

namespace InColUn.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        IDbContext dbContext;

        public UserRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Search for the user by user id.
        /// </summary>
        /// <param name="Id">User id</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        public User FindUserById(long Id)
        {
            var query = string.Format("select * from users where id = {0}", Id);
            return dbContext.QuerySingleOrDefault<User>(query);
        }

        /// <summary>
        /// Search for the user by user login string.
        /// </summary>
        /// <param name="loginString">User login string. Usually - email for local users, 
        /// "Profile Id" for external logins</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        public User FindUserByLoginString(string loginString)
        {
            //var query = string.Format("select * from users where login_string = '{0}'", loginString);
            return dbContext.QuerySingleOrDefault<User>
                ("select * from users where login_string = @login_string", new { login_string  = loginString });
        }

        /// <summary>
        /// Delete user from a database by Id
        /// </summary>
        /// <param name="Id">Unique user Id</param>
        public void DeleteUserById(long Id)
        {
            var deleteQuery = string.Format("DELETE FROM users WHERE id = {0}",Id);
            dbContext.Execute(deleteQuery);
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
        public bool CreateLocalUser(long Id, string name, string password_hash, long salt, string email)
        {
            var insertQuery = "INSERT INTO users (id, login_string, password_hash, salt, display_name, email, auth_provider)" +
                " VALUES (@id,@ls, @ps,@salt, @name, @email,'L')";

            return dbContext.Execute(insertQuery, new
            {
                id = Id,
                ls = email,
                ps = password_hash,
                salt = salt,
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
            var insertQuery = "INSERT INTO users (id, login_string, display_name, email, auth_provider)" +
                " VALUES (@id, @ls, @name, @email, @provider)";

            return dbContext.Execute(insertQuery, new
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
