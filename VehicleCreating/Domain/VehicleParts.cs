using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VehicleParts:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public Guid? VehicleFormulaId { get; set; }
        public VehicleFormula? VehicleFormula { get; set; }
        public string? image { get; set; }
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }

    }
}
