using ViewModels.ViewModels;
using System.Collections.Generic;

namespace Interfaces.Map
{
    public interface IUserMap
    {

        List<UserViewModel> GetAllUsers(int currentUserID);

        UserViewModel GetUserInfo(int userId);
        UserViewModel AuthorizeUser(string username, string password);

        UserPhotoViewModel GetUserPhoto(int ID, int currentUserID);

        IEnumerable<RoleViewModel> GetRoles(int currentUserID);

        RoleViewModel AddRole(RoleViewModel role, int currentUserID);
        RoleViewModel UpdateRole(RoleViewModel role, int currentUserID);
        bool DeleteRole(int roleID, int currentUserID);

        IEnumerable<ClaimViewModel> GetClaims(int currentUserID);


        void AddRoleClaim(int roleId, int claimId, int currentUserID);
        void DeleteRoleClaim(int roleId, int claimId, int currentUserID);

        void AddUser(UserViewModel user, int currentUserID);
        void RegisterUser(UserViewModel user, string password);
        void UpdateUser(UserViewModel user, int currentUserID);
        void DeleteUser(int userId, int currentUserID);
        void UpdateUserRole(int userId, int roleId, int currentUserID);
        void UpdateUserPassword(string username, string password, int currentUserID);

        void AddUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID);
        void UpdateUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID);

    }
}
