using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Service
{
    public interface IUserService
    {

        

        
        User AuthorizeUser(string username, string password);
        User GetUserInfo(int userID);

        List<User> GetAllUsers(int currentUserID);




        UserPhoto GetUserPhoto(int ID, int currentUserID);


        IEnumerable<Role> GetRoles(int currentUserID);

        IEnumerable<Claim> GetClaims(int currentUserID);

        Role AddRole(Role role, int currentUserID);
        Role UpdateRole(Role role, int currentUserID);
        bool DeleteRole(int roleID, int currentUserID);

        void AddRoleClaim(int roleId, int claimId, int currentUserID);
        void DeleteRoleClaim(int roleId, int claimId, int currentUserID);

        void AddUser(User user, int currentUserID);
        void RegisterUser(User user, string password);
        void UpdateUser(User user, int currentUserID);
        void DeleteUser(int userId, int currentUserID);
        void UpdateUserRole(int userId, int roleId, int currentUserID);
        void UpdateUserPassword(string username, string password, int currentUserID);

        void AddUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID);
        void UpdateUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID);
    }
}
