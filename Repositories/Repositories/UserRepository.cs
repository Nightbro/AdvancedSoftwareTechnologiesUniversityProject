using Microsoft.Extensions.Configuration;
using Interfaces.Repository;
using Models;
using Repositories.dboModels;
using Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {

        private ClaimQuery _claimQuery;
        private RoleClaimQuery _roleClaimQuery;
        private RoleQuery _roleQuery;
        private UserQuery _userQuery;
        private UserPhotoQuery _userPhotoQuery;
        private UserPasswordQuery userPasswordQuery;
        public UserRepository(IConfiguration config)
        {
            _claimQuery = new ClaimQuery(config);
            _roleClaimQuery = new RoleClaimQuery(config);
            _roleQuery = new RoleQuery(config);
            _userQuery = new UserQuery(config);
            _userPhotoQuery = new UserPhotoQuery(config);
            userPasswordQuery = new UserPasswordQuery(config);
        }

        #region Convertors
        User convertToUser(dboUser user)
        {
            var output = new User();
            output.Email = user.Email;
            output.FirstName = user.FirstName;
            output.Username = user.Username;
            output.ID = user.ID;
            output.LastName = user.LastName;
            output.RoleID = user.RoleID;
            return output;
        }
        List<User> convertToUser(List<dboUser> users)
        {
            var output = new List<User>();
            users.ForEach(x =>
            {
                output.Add(convertToUser(x));
            });
            return output;
        }
        Role convertToRole(dboRole role)
        {
            var output = new Role();
            output.ID = role.ID;
            output.Name = role.Name;
            return output;
        }
        List<Role> convertToRole(List<dboRole> roles)
        {
            var output = new List<Role>();
            roles.ForEach(x =>
            {
                output.Add(convertToRole(x));
            });
            return output;
        }
        List<User> convertToUser(List<dboUser> users, List<dboRole> roles)
        {
            var output = new List<User>();
            users.ForEach(x =>
            {
                var user = convertToUser(x);
                user.Role = convertToRole(roles.Where(r => r.ID == x.RoleID).FirstOrDefault());
                output.Add(user);
            });
            return output;
        }
        dboUser convertFromUser(User user)
        {
            if (user == null) return new dboUser();
            return new dboUser()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Username = user.Username,
                ID = user.ID,
                LastName = user.LastName,
                RoleID = user.RoleID
            };
        }
        Claim convertToClaim(dboClaim claim)
        {
            var output = new Claim();
            output.ID = claim.ID;
            output.Name = claim.Name;
            output.Description = claim.Description;
            return output;
        }
        List<Claim> convertToClaim(List<dboClaim> claims)
        {
            var output = new List<Claim>();
            claims.ForEach(x =>
            {
                output.Add(convertToClaim(x));
            });
            return output;
        }
        #endregion

        public List<User> GetAllUsers()
        {
            var users = _userQuery.GetAll();
            var roles = _roleQuery.GetAll();

            return convertToUser(users, roles);
        }


        public User GetUserInfo(int userID)
        {
            var user = _userQuery.Get(userID);
            if (user == null) return null;
            var output = convertToUser(user);
            var role = _roleQuery.Get(user.RoleID);
            if (role == null) return output;
            output.Role = convertToRole(role);
            var roleClaims = _roleClaimQuery.Get(role.ID);
            if (roleClaims == null) return output;
            var listOfClaimIDs = roleClaims.Select(x => x.ClaimID).ToList();

            var claims = _claimQuery.GetAll();
            var containedClaims = claims.Where(claim => listOfClaimIDs.Contains(claim.ID)).ToList();

            output.Role.Claims = convertToClaim(containedClaims);
            return output;
        }
        public UserPhoto convertToUserPhoto(dboUserPhoto photo)
        {
            var output = new UserPhoto();
            output.ImageFile = photo.ImageFile;
            output.UserID = photo.UserID;
            output.ImageName = photo.ImageName;
            output.OriginalFormat = photo.OriginalFormat;

            return output;

        }
        public dboRole convertFromRole(Role role)
        {
            return new dboRole()
            {
                IsAdmin = false,
                ID = role.ID,
                Name = role.Name

            };
        }




        public User AuthorizeUser(string username, string password)
        {
            var ind = userPasswordQuery.AuthorizeUser(new dboUserPassword() { Username = username, Password = password });
            if (!ind) throw new Exception("Wrong username and password combination");
            return convertToUser(_userQuery.GetByUsername(username));

        }

        public UserPhoto GetUserPhoto(int userId)
        {
            var userPhoto = _userPhotoQuery.Get(userId);

            return convertToUserPhoto(userPhoto);
        }

        public IEnumerable<Role> GetRoles()
        {
            var dboroles = _roleQuery.GetAll();
            var claims = _claimQuery.GetAll();
            var roles = convertToRole(dboroles);

            roles.ForEach(role =>
            {
                var roleClaims = _roleClaimQuery.Get(role.ID);
                if (roleClaims != null)
                {
                    var listOfClaimIDs = roleClaims.Select(x => x.ClaimID).ToList();
                    var containedClaims = claims.Where(claim => listOfClaimIDs.Contains(claim.ID)).ToList();
                    role.Claims = convertToClaim(containedClaims);
                }
            });


            return roles;
        }

        public IEnumerable<Claim> GetClaims()
        {
            var claims = _claimQuery.GetAll();
            return convertToClaim(claims);
        }

        public Role AddRole(Role role)
        {
            var newRole = convertToRole(_roleQuery.Add(convertFromRole(role)));
            if (newRole == null || newRole.Name != role.Name) throw new System.Exception("New role was not saved properly. Please try again");
            return newRole;
        }

        public Role UpdateRole(Role role)
        {
            return convertToRole(_roleQuery.Update(convertFromRole(role)));
        }

        public bool DeleteRole(int roleID)
        {
            return _roleQuery.Delete(roleID);
        }

        public void AddRoleClaim(int roleId, int claimId)
        {
                var role = new dboRoleClaim()
                {
                    RoleID = roleId,
                    ClaimID = claimId
                };
                _roleClaimQuery.Create(role);
        }

        public void DeleteRoleClaim(int roleId, int claimId)
        {
            var role = new dboRoleClaim()
            {
                RoleID = roleId,
                ClaimID = claimId
            };
            _roleClaimQuery.Delete(role);
        }

        private string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
        public void AddUser(User user)
        {
            _userQuery.Create(convertFromUser(user));
            var userId = _userQuery.GetByUsername(user.Username).ID;
  
            userPasswordQuery.Create(new dboUserPassword()
            {
                Username = user.Username,
                Password = "b43f1d28a3dbf30070bf1ae7c88ee2784047fc86d7be8620c8510debbd8555b3ef0b96376a4dd494ae0561580274bcf7a3069f5c0beceff63d1237a13d4d72b7"
            });
            _userPhotoQuery.Create(new dboUserPhoto()
            {
                UserID = userId,
                OriginalFormat = "image/jpg",
                ImageFile = Convert.FromBase64String("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"),
                ImageName = user.Username
            });

        }


        public void RegisterUser(User user, string password)
        {
            _userQuery.Create(convertFromUser(user));
            var userId = _userQuery.GetByUsername(user.Username).ID;
            userPasswordQuery.Create(new dboUserPassword()
            {
                Username = user.Username,
                Password = password
            });
            _userPhotoQuery.Create(new dboUserPhoto()
            {
                UserID = userId,
                OriginalFormat = "image/jpg",
                ImageFile = Convert.FromBase64String("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"),
                ImageName = user.Username
            });

        }

        public void UpdateUser(User user)
        {
            _userQuery.Update(convertFromUser(user));
        }

        public void DeleteUser(int userId)
        {
            var user = _userQuery.Get(userId);
            userPasswordQuery.DeleteById(user.Username);
            _userPhotoQuery.DeleteById(userId);
            _userQuery.Delete(userId);
        }

        public void UpdateUserRole(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPassword(string username, string password)
        {
            userPasswordQuery.Update(new dboUserPassword() { Password = password, Username = username });

        }

        public void AddUserPhoto(int userId, string originalFormat, byte[] blob)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserPhoto(int userId, string originalFormat, byte[] blob)
        {
            _userPhotoQuery.Update(new dboUserPhoto(){  
                ImageFile = blob,
                UserID = userId,
                OriginalFormat= originalFormat,
                ImageName = "UserRepositoryCreated"
            });
        }
    }
}
