using System;
using InColUn.Data.Models;

namespace InColUn.Data.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Search for the user by user id.
        /// </summary>
        /// <param name="Id">User id</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        User FindUserById(long Id);

        /// <summary>
        /// Search for the user by user login string.
        /// </summary>
        /// <param name="loginString">User login string. Usually - email for local users, 
        /// "Profile Id" for external logins</param>
        /// <returns>Return user. If user was not found - returns null.</returns>
        User FindUserByLoginString(string loginString);

        /// <summary>
        /// Delete user from a database by Id
        /// </summary>
        /// <param name="Id">Unique user Id</param>
        void DeleteUserById(long Id);

        /// <summary>
        /// Create local user
        /// Email will be used as indexed login_string
        /// </summary>
        /// <param name="Id">User id. Should be unique</param>
        /// <param name="name">User name. Will be displayed in UX</param>
        /// <param name="password">User password. Will be converted into hash</param>
        /// <param name="email">Uer email</param>
        /// <returns></returns>
        bool CreateLocalUser(
            long Id, string name,
            string password_hash,
            long salt,
            string email);

        /// <summary>
        /// Create user from external auth proviced
        /// </summary>
        /// <param name="Id">Unique user id.</param>
        /// <param name="loginString">User unique login string. Useally in form of "id" from external auth provider.</param>
        /// <param name="displayName">User Display name if available. Can be null.</param>
        /// <param name="email">User email if available. Can be null/empty.</param>
        /// <param name="externalProvider">External provider Id. 'G' - Google, ' F' - Facebook</param>
        /// <returns></returns>
        bool CreateExternalUser(
            long Id,
            string loginString,
            string displayName,
            string email,
            string externalProvider);
    }
}
