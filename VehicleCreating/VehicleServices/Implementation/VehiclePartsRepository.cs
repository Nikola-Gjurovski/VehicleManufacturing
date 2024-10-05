using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;
using VehicleServices.Interface;

namespace VehicleServices.Implementation
{
    public class VehiclePartsRepository:IVehicleParts
    {
        private readonly IRepository<VehicleParts> _reservationRepository;
        public VehiclePartsRepository(IRepository<VehicleParts> productRepository)
        {
            _reservationRepository = productRepository;
        }
        public void CreateNewReservation(VehicleParts p)
        {
            _reservationRepository.Insert(p);
        }

        public void DeleteReservation(Guid id)
        {
            var r = _reservationRepository.Get(id);
            _reservationRepository.Delete(r);
        }

        public List<VehicleParts> GetAllReservations()
        {
            return _reservationRepository.GetAll().ToList();
        }

        public VehicleParts GetDetailsForReservation(Guid? id)
        {
            var r = _reservationRepository.Get(id);
            return r;
        }

        public void UpdateExistingReservation(VehicleParts p)
        {
            _reservationRepository.Update(p);
        }

       
    }
}
