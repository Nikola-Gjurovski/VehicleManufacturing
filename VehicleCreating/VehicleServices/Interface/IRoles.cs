using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServices.Interface
{
    public interface IRoles
    {
        bool check(string userId);
        List<VehicleApplicationUser> getUsers();
        void postUser(string Id);
        VehicleApplicationUser getWantedUser(string userId);
    }
}
