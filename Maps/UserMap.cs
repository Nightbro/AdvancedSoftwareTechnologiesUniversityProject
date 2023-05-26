using AutoMapper;
using Interfaces.Map;
using Interfaces.Service;
using Models;
using ViewModels.ViewModels;
using System;
using System.Collections.Generic;

namespace Maps
{
    public class UserMap: IUserMap
    {

        IUserService userService;
        IMapper iMapper;
        public UserMap(IUserService service)
        {
            userService = service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<ClaimViewModel, Claim>();
                cfg.CreateMap<RoleViewModel, Role>();
                cfg.CreateMap<UserPhotoViewModel, UserPhoto>();

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Claim, ClaimViewModel>();
                cfg.CreateMap<Role, RoleViewModel>();
                cfg.CreateMap<UserPhoto, UserPhotoViewModel>();
            });
            config.AssertConfigurationIsValid();
            iMapper = config.CreateMapper();
        }

        public List<UserViewModel> GetAllUsers(int currentUserId)
        {
            return iMapper.Map<List<User>, List<UserViewModel>>(userService.GetAllUsers(currentUserId));
        }

        public UserViewModel GetUserInfo(int userId)
        {
            return iMapper.Map<User, UserViewModel>(userService.GetUserInfo(userId));
        }

        public UserViewModel AuthorizeUser(string username, string password)
        {
            return iMapper.Map<User, UserViewModel>(userService.AuthorizeUser(username, password));
        }


        public UserPhotoViewModel GetUserPhoto(int ID, int currentUserID)
        {
            return iMapper.Map<UserPhoto, UserPhotoViewModel>(userService.GetUserPhoto(ID, currentUserID));
        }

        public IEnumerable<RoleViewModel> GetRoles(int currentUserID)
        {
            return iMapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(userService.GetRoles(currentUserID));
        }

        public IEnumerable<ClaimViewModel> GetClaims(int currentUserID)
        {
            return iMapper.Map<IEnumerable<Claim>, IEnumerable<ClaimViewModel>>(userService.GetClaims(currentUserID));
        }



        public RoleViewModel AddRole(RoleViewModel role, int currentUserID)
        {
            return iMapper.Map<Role, RoleViewModel>(userService.AddRole(iMapper.Map<RoleViewModel, Role>(role), currentUserID));

        }

        public RoleViewModel UpdateRole(RoleViewModel role, int currentUserID)
        {
            return iMapper.Map<Role, RoleViewModel>(userService.UpdateRole(iMapper.Map<RoleViewModel, Role>(role), currentUserID));

        }

        public bool DeleteRole(int roleID, int currentUserID)
        {
            return userService.DeleteRole(roleID, currentUserID);
        }

        public void AddRoleClaim(int roleId, int claimId, int currentUserID)
        {
            userService.AddRoleClaim(roleId, claimId, currentUserID);
        }

        public void DeleteRoleClaim(int roleId, int claimId, int currentUserID)
        {
            userService.DeleteRoleClaim(roleId, claimId, currentUserID);

        }

        public void AddUser(UserViewModel user, int currentUserID)
        {
            userService.AddUser(iMapper.Map<UserViewModel, User>(user), currentUserID);

        }
        public void RegisterUser(UserViewModel user, string password)
        {
            userService.RegisterUser(iMapper.Map<UserViewModel, User>(user), password);

        }


        public void UpdateUser(UserViewModel user, int currentUserID)
        {
            userService.UpdateUser(iMapper.Map<UserViewModel, User>(user), currentUserID);

        }

        public void DeleteUser(int userId, int currentUserID)
        {
            userService.DeleteUser(userId, currentUserID);

        }

        public void UpdateUserRole(int userId, int roleId, int currentUserID)
        {
            userService.UpdateUserRole(userId, roleId, currentUserID);

        }

        public void UpdateUserPassword(string username, string password, int currentUserID)
        {
            userService.UpdateUserPassword(username, password, currentUserID);

        }

        public void AddUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID)
        {
            userService.AddUserPhoto(userId, originalFormat, blob, currentUserID);

        }

        public void UpdateUserPhoto(int userId, string originalFormat, byte[] blob, int currentUserID)
        {
            userService.UpdateUserPhoto(userId, originalFormat, blob, currentUserID);

        }
    }
}
