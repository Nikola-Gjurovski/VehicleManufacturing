using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShoppingVehicleFormulaTypeDto
    {
        public Guid Id { get; set; }
        public List<VehicleFormula>vehicleFormulas { get; set; }
        
        public string VehicleName { get; set; }
        public string VehicleColor { get; set; }
        public string CompanyName { get; set; }
    }
}
