using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VehicleServices.Interface;


namespace VehicleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleFormula _VehicleFormulaService;
        private readonly IRoles roles;

        public HomeController(ILogger<HomeController> logger,IVehicleFormula vehicleFormula,IRoles roles)
        {
            _logger = logger;
            _VehicleFormulaService = vehicleFormula;
            this.roles = roles;
        }

        public IActionResult Index()
        {
            HomePageDto homePageDto = new HomePageDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                

                
                    homePageDto.User=roles.getWantedUser(userId);
                
            }
            homePageDto.VehicleFormula=_VehicleFormulaService.GetAllVehicleFormulas();
            return View(homePageDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GalleryFull()
        {
            return View();
        }
    }
}
