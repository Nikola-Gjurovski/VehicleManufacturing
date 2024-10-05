using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServices.Interface;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;

namespace VehicleWeb.Controllers
{
    public class VehiclePartsController : Controller
    {
        private readonly IVehicleParts _reservationService;
        private readonly IVehicleFormula _vehicleFormula;

        public VehiclePartsController(IVehicleParts reservationService, IVehicleFormula vehicleFormula)
        {
            _reservationService = reservationService;
            _vehicleFormula = vehicleFormula;
        }

        public IActionResult Index()
        {
            // Ensure VehicleFormula is loaded along with VehicleParts
            var vehicleParts = _reservationService.GetAllReservations();
            foreach (var part in vehicleParts)
            {
                part.VehicleFormula = _vehicleFormula.GetDetailsForVehicleFormula(part.VehicleFormulaId.Value);
            }
            return View(vehicleParts);

        }

        public IActionResult Details(Guid id)
        {
            var details = _reservationService.GetDetailsForReservation(id);
            details.VehicleFormula = _vehicleFormula.GetDetailsForVehicleFormula(details.VehicleFormulaId.Value);
            return View(details);
        }

        public IActionResult Create()
        {
            ViewData["VehicleFormulaId"] = new SelectList(_vehicleFormula.GetAllVehicleFormulas(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,Manufacturer,VehicleFormulaId,image")] VehicleParts vehicleParts)
        {
            if (ModelState.IsValid)
            {
                vehicleParts.Id = Guid.NewGuid();
                _reservationService.CreateNewReservation(vehicleParts);
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleFormulaId"] = new SelectList(_vehicleFormula.GetAllVehicleFormulas(), "Id", "Name", vehicleParts.VehicleFormulaId);
            return View(vehicleParts);
        }

        public IActionResult Edit(Guid id)
        {
            var product = _reservationService.GetDetailsForReservation(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["VehicleFormulaId"] = new SelectList(_vehicleFormula.GetAllVehicleFormulas(), "Id", "Name", product.VehicleFormulaId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Price,Description,Manufacturer,VehicleFormulaId,image")] VehicleParts reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _reservationService.UpdateExistingReservation(reservation);
                }
                catch (Exception ex)
                {
                    // Handle exception
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleFormulaId"] = new SelectList(_vehicleFormula.GetAllVehicleFormulas(), "Id", "Name", reservation.VehicleFormulaId);
            return View(reservation);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetDetailsForReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Ensure VehicleFormula is loaded along with VehicleParts
            var vehicleParts = _reservationService.GetAllReservations();
            foreach (var part in vehicleParts)
            {
                part.VehicleFormula = _vehicleFormula.GetDetailsForVehicleFormula(part.VehicleFormulaId.Value);
            }
            return Json(new {data=vehicleParts});

        }
        #endregion
    }
}

