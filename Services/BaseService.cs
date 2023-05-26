using Interfaces.Repository;
using Interfaces.Service;
using Models;
using System.Collections.Generic;
using System.Linq;
namespace Services
{
    public class BaseService
    {
        private IUserRepository repository;

        private int curentUserId;
        public User curentUser;

        public BaseService(IUserRepository userRepository)
        {
            repository = userRepository;
        }
        public void SetUser(int userId )
        {
            if (userId == curentUserId) return;
            curentUserId = userId;
            curentUser = repository.GetUserInfo(userId);
        }
        public void CheckPermission( int claimId)
        {
            if (!HasClaim(claimId)) throw new System.Exception("User does not have permission to see this");
        }

        public bool HasClaim(int claimId)
        {
            if (curentUser == null) throw new System.Exception("User is not properly authenticated");
            return curentUser.Role.Claims.Any(x => x.ID == claimId);
        }


    }
}
