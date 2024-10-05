using Domain;
using Domain.DTO;
using ExcelDataReader;
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System.Security.Claims;
using System.Text;
using VehicleServices.Interface;

namespace VehicleWeb.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IVehicleFormula _VehicleFormulaService;
        private readonly IVehicleParts _vehicleParts;
        private readonly IRoles roles;
        private readonly IShopping _shopping;
        private readonly IOrderService _orderService;
        public ShoppingController(IVehicleFormula vehicleFormulaService, IRoles roles, IShopping shopping, IVehicleParts vehicleParts, IOrderService orderService)
        {
            _VehicleFormulaService = vehicleFormulaService;
            this.roles = roles;
            _shopping = shopping;
            _vehicleParts = vehicleParts;
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            ShoppingVehicleFormulaTypeDto shoppingVehicleFormulaTypeDto = new ShoppingVehicleFormulaTypeDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            shoppingVehicleFormulaTypeDto.vehicleFormulas = _VehicleFormulaService.GetAllVehicleFormulas();
            return View(shoppingVehicleFormulaTypeDto);


        }
        [HttpPost]
        public IActionResult Index(ShoppingVehicleFormulaTypeDto shoppingVehicleFormulaTypeDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (shoppingVehicleFormulaTypeDto.CompanyName == null || shoppingVehicleFormulaTypeDto.VehicleName == null || shoppingVehicleFormulaTypeDto.VehicleColor == null)
            {
                TempData["Message"] = "Please give us more information about your vehicle";
                return RedirectToAction("Index");
            }
            _shopping.FilterType(shoppingVehicleFormulaTypeDto, userId);
            return RedirectToAction("actionResult");
        }
        public IActionResult actionResult()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {



                var user = roles.getWantedUser(userId);
                ShoppingVehicleTypeDto shoppingVehicleTypeDto_ = new ShoppingVehicleTypeDto();
                shoppingVehicleTypeDto_.chassis = _vehicleParts.GetAllReservations()
           .Where(x => x.Description.Contains("Chassis", StringComparison.OrdinalIgnoreCase)
                       && x.VehicleFormula.Name == user.ShoppingCart.Type)
           .ToList();
                shoppingVehicleTypeDto_.engines = _vehicleParts.GetAllReservations()
          .Where(x => x.Description.Contains("Engine", StringComparison.OrdinalIgnoreCase)
                      && x.VehicleFormula.Name == user.ShoppingCart.Type)
          .ToList();
                shoppingVehicleTypeDto_.wheels = _vehicleParts.GetAllReservations()
          .Where(x => x.Description.Contains("Wheel", StringComparison.OrdinalIgnoreCase)
                      && x.VehicleFormula.Name == user.ShoppingCart.Type)
          .ToList();
                shoppingVehicleTypeDto_.doors = _vehicleParts.GetAllReservations()
          .Where(x => x.Description.Contains("Door", StringComparison.OrdinalIgnoreCase)
                      && x.VehicleFormula.Name == user.ShoppingCart.Type)
          .ToList();

                return View(shoppingVehicleTypeDto_);



            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });

        }
        [HttpPost]
        public IActionResult actionResult(ShoppingVehicleTypeDto shoppingVehicleTypeDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shopping.FullShoppingCard(shoppingVehicleTypeDto, userId);
            return RedirectToAction("UserShoppingCart");
        }
        public IActionResult UserShoppingCart()
        {
            UserShoppingCartDto userShoppingCartDto = new UserShoppingCartDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {



                var user = roles.getWantedUser(userId);
                userShoppingCartDto.ShoppingCart = user.ShoppingCart;
                return View(userShoppingCartDto);

            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        public IActionResult DeleteShoppingCard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {



                _shopping.DeleteShoppingCard(userId);
                return RedirectToAction("UserShoppingCart");

            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        }
        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {



                _shopping.Order(userId);
                return RedirectToAction("UserShoppingCart");

            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        }
        public IActionResult ShowOrders()
        {
            OrderDto order = new OrderDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {



                var user = roles.getWantedUser(userId);
                order.Orders = user.Orders.ToList();
                return View(order);

            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51NmkhEJxUrQu8FiUVJ2LVLGFEthwhiXAqHMTIsSnil72vOHqkrtb9qLUen9yngiPjNMwfhrd7cwH6cY9bGFY8lJO00WoAqgplr";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserShoppingCartDto userShoppingCartDto = new UserShoppingCartDto();
            var user = roles.getWantedUser(userId);

            var order = user.ShoppingCart;

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.Price) * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("ShowOrders");
            }
            else
            {
                return RedirectToAction("Failed", "Proba");
            }
        }
        public IActionResult OrdersAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            if (roles.check(userId))
            {
                return View(_orderService.GetAllOrders());
            }
            return RedirectToAction("NotActive", "Proba");

        }
        public IActionResult OrdersAdminDetails(Guid id)
        {
            BaseEntity baseEntity = new BaseEntity();
            baseEntity.Id = id;
            return View(_orderService.GetOrderDetails(baseEntity));
        }
        public IActionResult ExcellReader()
        {
            return View();
        }
        public FileContentResult CreateInvoice(Guid id)
        {
            BaseEntity baseEntity = new BaseEntity();
            baseEntity.Id = id;
            var result = _orderService.GetOrderDetails(baseEntity);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");

            if (!System.IO.File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Invoice template not found: {templatePath}");
            }

            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.ApplicationUser.UserName);

            StringBuilder sb = new StringBuilder();
            foreach (var item in result.ShoppingCart.ProductShoppingCarts)
            {
                sb.AppendLine($"Product {item.Name} has manufacturer {item.Manufacturer} with price {item.Price}$");
            }
            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", $"{result.ShoppingCart.Price}$");
            document.Content.Replace("{{VehicleName}}", result.ShoppingCart.VehicleName);
            document.Content.Replace("{{VehicleType}}", result.ShoppingCart.Type);

            using (var stream = new MemoryStream())
            {
                document.Save(stream, new PdfSaveOptions());
                return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
            }
        }


        public IActionResult ImportUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected or empty");
            }

            string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
            string pathToUpload = Path.Combine(uploadDirectory, file.FileName);

            try
            {
                // Ensure directory exists
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Save the file
                using (FileStream fileStream = System.IO.File.Create(pathToUpload))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                // Process the file
                List<VehicleFormula> model = getAllUsersFromFile(pathToUpload);
                foreach (var item in model)
                {
                    var user = new VehicleFormula
                    {
                        Name = item.Name,
                        chassis = item.chassis,
                        engines = item.engines,
                        doors = item.doors,
                        wheels = item.wheels,
                        image = item.image,
                    };

                    _VehicleFormulaService.CreateNewVehicleFormula(user);
                }

                return RedirectToAction("Index", "VehicleFormulas");
            }
            catch (Exception ex)
            {
                // Log the exception

                return StatusCode(500, "Internal server error");
            }
        }

        private List<VehicleFormula> getAllUsersFromFile(string filePath)
        {
            List<VehicleFormula> users = new List<VehicleFormula>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            try
            {
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            users.Add(new VehicleFormula
                            {
                                Name = reader.GetValue(0).ToString(),
                                engines = Convert.ToInt32(reader.GetValue(1)),
                                chassis = Convert.ToInt32(reader.GetValue(2)),
                                doors = Convert.ToInt32(reader.GetValue(3)),
                                wheels = Convert.ToInt32(reader.GetValue(4)),
                                image = reader.GetValue(5).ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception

                throw;
            }

            return users;
        }
        [HttpPost]
        public IActionResult SetIsDone(Guid id)
        {
            BaseEntity baseEntity = new BaseEntity();
            baseEntity.Id = id;
            // Find the order by id and set IsDone to true
            var order = _orderService.GetOrderDetails(baseEntity);
            if (order != null)
            {
                order.IsDone = true; // Set IsDone to true
                _orderService.UpdateOrder(order); // Update the order in your service/repository
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Ensure VehicleFormula is loaded along with VehicleParts
            var vehicleParts = _orderService.GetAllOrders();

            return Json(new { data = vehicleParts });

        }
        #endregion



    }
}
