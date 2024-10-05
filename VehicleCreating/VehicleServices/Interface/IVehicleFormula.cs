using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServices.Interface
{
    public interface IVehicleFormula
    {
        List<VehicleFormula> GetAllVehicleFormulas();
        VehicleFormula GetDetailsForVehicleFormula(Guid? id);
        void CreateNewVehicleFormula(VehicleFormula p);
        void UpdeteExistingVehicleFormula(VehicleFormula p);
        void DeleteVehicleFormula(Guid id);
    }
}
