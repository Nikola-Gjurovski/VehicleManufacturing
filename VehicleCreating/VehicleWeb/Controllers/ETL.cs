using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using VehicleReposiotry.Interface;
using VehicleServices.Interface;

namespace VehicleWeb.Controllers
{
    public class ETL : Controller
    {
        private readonly IETLRepository _eTLRepository;

        public ETL(IETLRepository eTLRepository)
        {
            _eTLRepository = eTLRepository;
           
        }
        public IActionResult Index()
        {
           
            return View(_eTLRepository.GetAll().ToList());
        }
       
    }
}
