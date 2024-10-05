using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;
using VehicleServices.Interface;

namespace VehicleServices.Implementation
{
    public class RoleRepository : IRoles
    {
        private readonly IUserRepository _userRepository;
        public RoleRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool check(string userId)
        {
            var loggedUser = _userRepository.Get(userId);
            return loggedUser.IsAdmin==true;
        }

        public List<VehicleApplicationUser> getUsers()
        {
           return _userRepository.GetAll().Where(x=>x.IsAdmin==false).ToList();
        }

        public VehicleApplicationUser getWantedUser(string userId)
        {
           return _userRepository.Get(userId);
        }

        public void postUser(string Id)
        {
            var user = _userRepository.Get(Id);
            user.IsAdmin=true;
            _userRepository.Update(user);
        }
    }
}
