using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public VehicleApplicationUser ApplicationUser { get; set; }
        public Guid ShoppingCardId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public bool? IsDone { get; set; }
    }
}
