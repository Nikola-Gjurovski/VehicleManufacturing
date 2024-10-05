using Domain;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;
using VehicleServices.Interface;

namespace VehicleServices.Implementation
{
    public class ShoppingRepository : IShopping
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _reservationRepository;
        private readonly IRepository<VehicleFormula> _vehicleFormula;
        private readonly IRepository<VehicleParts> _vehiclePartsRepository;
        private readonly IRepository<Order> _orderRepository;

        public ShoppingRepository(IUserRepository userRepository, IRepository<ShoppingCart> reservationRepository,IRepository<VehicleFormula>vehicleFormula,IRepository<VehicleParts>vehiclePartsRepository,IRepository<Order>orderRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _vehicleFormula = vehicleFormula;
            _vehiclePartsRepository=vehiclePartsRepository;
            _orderRepository=orderRepository;
        }

        public void DeleteShoppingCard(string userId)
        {
            var loggedUser = _userRepository.Get(userId);
            loggedUser.ShoppingCart=null;
            var card=_reservationRepository.GetAll().Where(x=>x.OwnerId==userId).First();
            _reservationRepository.Delete(card);
            _userRepository.Update(loggedUser);
        }

        public void FilterType(ShoppingVehicleFormulaTypeDto shoppingVehicleFormulaTypeDto, string userId)
        {
            var loggedUser = _userRepository.Get(userId);
            var formula = _vehicleFormula.Get(shoppingVehicleFormulaTypeDto.Id);
            ShoppingCart shoppingCart=new ShoppingCart();
            shoppingCart.Id=Guid.NewGuid();
            shoppingCart.OwnerId=userId;
            shoppingCart.Owner = loggedUser;
            shoppingCart.Type = formula.Name;
            shoppingCart.CompanyName=shoppingVehicleFormulaTypeDto.CompanyName;
            shoppingCart.VehicleName=shoppingVehicleFormulaTypeDto.VehicleName;
            shoppingCart.VehicleColor = shoppingVehicleFormulaTypeDto.VehicleColor;
            loggedUser.ShoppingCart= shoppingCart;
            _reservationRepository.Insert(shoppingCart);
            _userRepository.Update(loggedUser);
            


        }

        public void FullShoppingCard(ShoppingVehicleTypeDto shoppingVehicleTypeDto, string userId)
        {
            var loggedUser = _userRepository.Get(userId);
            var chassis=_vehiclePartsRepository.Get(shoppingVehicleTypeDto.ChassisId);
            var engines = _vehiclePartsRepository.Get(shoppingVehicleTypeDto.EngineId);
            var doors = _vehiclePartsRepository.Get(shoppingVehicleTypeDto.DoorId);
            var wheels = _vehiclePartsRepository.Get(shoppingVehicleTypeDto.WheelsId);
            var formula=_vehicleFormula.GetAll().Where(x=>x.Name==loggedUser.ShoppingCart.Type).First();
            loggedUser.ShoppingCart.ProductShoppingCarts = new List<VehicleParts>();
            loggedUser.ShoppingCart.ProductShoppingCarts.Add(wheels);
            loggedUser.ShoppingCart.ProductShoppingCarts.Add(doors);
            loggedUser.ShoppingCart.ProductShoppingCarts.Add(chassis);
            loggedUser.ShoppingCart.ProductShoppingCarts.Add(engines);
            int price=Convert.ToInt32(chassis.Price*formula.chassis +engines.Price*formula.engines+doors.Price * formula.doors +wheels.Price*formula.wheels+10000);
            loggedUser.ShoppingCart.Price = price;
            _userRepository.Update(loggedUser);

        }

        public List<Order> getAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }

        public void Order(string userId)
        {
            var loggedUser = _userRepository.Get(userId);
            Order order=new Order();
            order.UserId = userId;
            order.ApplicationUser= loggedUser;
            order.ShoppingCardId = loggedUser.ShoppingCart.Id;
            order.ShoppingCart=loggedUser.ShoppingCart;
            order.IsDone = false;
            loggedUser.Orders.Add(order);
            loggedUser.ShoppingCart = null;
            _orderRepository.Insert(order);
            _userRepository.Update(loggedUser);
        }
    }
}
