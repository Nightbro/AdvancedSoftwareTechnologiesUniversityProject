using Models;
using System.Collections.Generic;

namespace Interfaces.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();


        User GetUserInfo(int userID);

        User AuthorizeUser(string username, string password);
        UserPhoto GetUserPhoto(int userId);

        IEnumerable<Role> GetRoles();

        IEnumerable<Claim> GetClaims();

        Role AddRole(Role role);
        Role UpdateRole(Role role);
        bool DeleteRole(int roleID);

        void AddRoleClaim(int roleId, int claimId);
        void DeleteRoleClaim(int roleId, int claimId);

        void AddUser(User user);
        void RegisterUser(User user, string password);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void UpdateUserRole(int userId, int roleId);
        void UpdateUserPassword(string username, string password);

        void AddUserPhoto(int userId, string originalFormat, byte[] blob);
        void UpdateUserPhoto(int userId, string originalFormat, byte[] blob);

    }
}
