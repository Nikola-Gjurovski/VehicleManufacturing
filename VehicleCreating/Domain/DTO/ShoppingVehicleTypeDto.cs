using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShoppingVehicleTypeDto
    {
        public Guid ChassisId { get; set; }
        public List<VehicleParts> chassis { get; set; }
        public Guid EngineId { get; set; }
        public List<VehicleParts> engines { get; set; }
        public Guid DoorId { get; set; }
        public List<VehicleParts> doors { get; set; }
        public Guid WheelsId { get; set; }
        public List<VehicleParts> wheels { get; set; }

    }
}
