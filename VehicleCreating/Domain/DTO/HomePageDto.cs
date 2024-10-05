using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class HomePageDto
    {
        public VehicleApplicationUser User { get; set; }
        public List<VehicleFormula> VehicleFormula { get; set; }
    }
}
