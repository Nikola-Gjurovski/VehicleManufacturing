using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VehicleFormula:BaseEntity
    {
       public string Name { get; set; }
        public int engines {  get; set; }
        public int chassis { get; set; }
        public int doors { get; set; }
        public int wheels { get; set; }
        public string image { get; set; }
        public List<VehicleParts>? parts { get; set; }
    }
}
