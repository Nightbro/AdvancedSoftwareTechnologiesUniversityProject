using Interfaces.Repository;
using Interfaces.Service;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class UserService : BaseService, IUserService 
    {
        private IUserRepository repository;
        public UserService(IUserRepository userRepository) :base(userRepository)
        {
            repository = userRepository;
        }

        public List<User> GetAllUsers(int currentUserId)
        {
            SetUser(currentUserId);
            CheckPermission((int)Claims.ManageUsers);
            return repository.GetAllUsers() ;

            //return repository.GetAll();
        }

        public User GetUserInfo(int userID)
        {
            return repository.GetUserInfo(userID);
        }
        public User AuthorizeUser(string username, string password)
        {
            return repository.AuthorizeUser(username, password);
        }

        public UserPhoto GetUserPhoto(int ID, int currentUserID)
        {
            if (ID == -1) ID = currentUserID;
            return repository.GetUserPhoto(ID);
        }

        public IEnumerable<Role> GetRoles(int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            return repository.GetRoles();
        }

        public IEnumerable<Claim> GetClaims(int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            return repository.GetClaims();
        }

        public Role AddRole(Role role, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            return repository.AddRole(role);
        }

        public Role UpdateRole(Role role, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            return repository.UpdateRole(role);
        }


        public bool DeleteRole(int roleID, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            return repository.DeleteRole(roleID);
        }

        public void AddRoleClaim(int roleId, int claimId, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            repository.AddRoleClaim(roleId, claimId);
        }

        public void DeleteRoleClaim(int roleId, int claimId, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageRoles);
            repository.DeleteRoleClaim(roleId, claimId);
        }

        public void AddUser(User user, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageUsers);
            repository.AddUser(user);
        }

        public void RegisterUser(User user, string password)
        {
            repository.RegisterUser(user, password);
        }

        public void UpdateUserPassword(string username, string password, int currentUserID)
        {
            SetUser(currentUserID);
            if (username != curentUser.Username) CheckPermission((int)Claims.ManageUsers);
            repository.UpdateUserPassword(username, password);
        }

        public void UpdateUser(User user, int currentUserID)
        {
            SetUser(currentUserID);
            if (currentUserID != user.ID) CheckPermission((int)Claims.ManageUsers);

            repository.UpdateUser(user);
        }

        public void DeleteUser(int userId, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageUsers);
            repository.DeleteUser(userId);
        }

        public void UpdateUserRole(int userId, int roleId, int currentUserID)
        {
            SetUser(currentUserID);
            CheckPermission((int)Claims.ManageUsers);
            repository.UpdateUserRole(userId, roleId);
        }



        public void AddUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID)
        {

            repository.AddUserPhoto(userId, originalFormat, blob);
        }

        public void UpdateUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID)
        {
            repository.UpdateUserPhoto(userId, originalFormat, blob);
        }
    }
}
