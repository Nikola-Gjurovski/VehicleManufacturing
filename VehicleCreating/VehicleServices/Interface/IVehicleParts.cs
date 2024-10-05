using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServices.Interface
{
    public interface IVehicleParts
    {
        List<VehicleParts> GetAllReservations();
        VehicleParts GetDetailsForReservation(Guid? id);
        void CreateNewReservation(VehicleParts p);
        void UpdateExistingReservation(VehicleParts p);
        void DeleteReservation(Guid id);
    }
}
