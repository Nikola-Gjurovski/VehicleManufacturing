using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleReposiotry.Interface
{
    public interface IUserRepository
    {
        IEnumerable<VehicleApplicationUser> GetAll();
        VehicleApplicationUser Get(string? id);
        void Insert(VehicleApplicationUser entity);
        void Update(VehicleApplicationUser entity);
        void Delete(VehicleApplicationUser entity);
    }
}
