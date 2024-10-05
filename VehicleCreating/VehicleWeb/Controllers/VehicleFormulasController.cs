using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VehicleReposiotry;
using VehicleReposiotry.Migrations;
using VehicleServices.Interface;

namespace VehicleWeb.Controllers
{
    public class VehicleFormulasController : Controller
    {
        private readonly IVehicleFormula _VehicleFormulaService;
        private readonly IRoles roles;
        public VehicleFormulasController(IVehicleFormula vehicleFormula, IRoles roles)
        {
            _VehicleFormulaService = vehicleFormula;
            this.roles = roles;
        }

        // GET: VehicleFormulas
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            if (roles.check(userId))
            {
                return View(_VehicleFormulaService.GetAllVehicleFormulas());
            }
            return RedirectToAction("NotActive","Proba");
            
        }
        public async Task<IActionResult> SetAdmin()
        {
            UsersDto usersDto = new UsersDto();
            usersDto.users = roles.getUsers();
            return View(usersDto);
        }
        //[HttpPost]
        //public async Task<IActionResult> SetAdmin(UsersDto usersDto)
        //{
        //    roles.postUser(usersDto.Id);
        //    TempData["SuccessMessage"] = "User has been successfully set as admin.";
        //    return RedirectToAction("SetAdmin");
        //}
        [HttpPost]
        public async Task<IActionResult> SetAdmin(string Id)
        {
            roles.postUser(Id);
            TempData["SuccessMessage"] = "User has been successfully set as admin.";
            return RedirectToAction("SetAdmin");
        }

        // GET: VehicleFormulas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var VehicleFormula = _VehicleFormulaService.GetDetailsForVehicleFormula(id);
            if (VehicleFormula == null)
            {
                return NotFound();
            }

            return View(VehicleFormula);
        }

        // GET: VehicleFormulas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleFormulas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,engines,chassis,doors,wheels,image")] VehicleFormula vehicleFormula)
        {
            if (ModelState.IsValid)
            {
                vehicleFormula.Id = Guid.NewGuid();
                _VehicleFormulaService.CreateNewVehicleFormula(vehicleFormula);

                return RedirectToAction(nameof(Index));
            }
            return View(vehicleFormula);
        }

        // GET: VehicleFormulas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var VehicleFormula = _VehicleFormulaService.GetDetailsForVehicleFormula(id);
            if (VehicleFormula == null)
            {
                return NotFound();
            }
            return View(VehicleFormula);
        }

        // POST: VehicleFormulas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,engines,chassis,doors,wheels,image")] VehicleFormula vehicleFormula)
        {
            if (id != vehicleFormula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _VehicleFormulaService.UpdeteExistingVehicleFormula(vehicleFormula);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleFormula);
        }

        // GET: VehicleFormulas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var VehicleFormula = _VehicleFormulaService.GetDetailsForVehicleFormula(id);

            if (VehicleFormula == null)
            {
                return NotFound();
            }

            return View(VehicleFormula);
        }

        // POST: VehicleFormulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _VehicleFormulaService.DeleteVehicleFormula(id);

            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Ensure VehicleFormula is loaded along with VehicleParts
            var vehicleParts = _VehicleFormulaService.GetAllVehicleFormulas();
           
            return Json(new { data = vehicleParts });

        }
        #endregion
        #region API CALLS
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            // Ensure VehicleFormula is loaded along with VehicleParts
            var vehicleParts = roles.getUsers();

            return Json(new { data = vehicleParts });

        }
        #endregion


        //private bool VehicleFormulaExists(Guid id)
        //{
        //    return _context.VehicleFormula.Any(e => e.Id == id);
        //}
    }
}
