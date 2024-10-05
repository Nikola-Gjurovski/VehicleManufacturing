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
    public class VehicleFormulaRepository : IVehicleFormula
    {
        private readonly IRepository<VehicleFormula> _productRepository;
        public VehicleFormulaRepository(IRepository<VehicleFormula> productRepository)
        {
            _productRepository = productRepository;
        }



        public void CreateNewVehicleFormula(Domain.VehicleFormula p)
        {
            _productRepository.Insert(p);
        }

        public void DeleteVehicleFormula(Guid id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }

        public List<Domain.VehicleFormula> GetAllVehicleFormulas()
        {
            return _productRepository.GetAll().ToList();
        }

        public Domain.VehicleFormula GetDetailsForVehicleFormula(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public void UpdeteExistingVehicleFormula(Domain.VehicleFormula p)
        {
            _productRepository.Update(p);
        }
    }
}
