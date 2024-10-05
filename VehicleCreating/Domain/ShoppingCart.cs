using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ShoppingCart:BaseEntity
    {
        public string? OwnerId { get; set; }

        public virtual VehicleApplicationUser? Owner { get; set; }
        public string ?Type { get; set; }
        public virtual  ICollection<VehicleParts> ?ProductShoppingCarts { get; set; }
        public string ?VehicleName { get; set; }
        public string ?VehicleColor { get; set; }
        public string ?CompanyName { get; set; }
        public int ?Price { get; set; }
   
    }
}
