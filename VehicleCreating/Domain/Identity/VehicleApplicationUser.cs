using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public  class VehicleApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public bool? IsAdmin { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
