using Domain;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServices.Interface
{
    public interface IShopping
    {
        void FilterType(ShoppingVehicleFormulaTypeDto shoppingVehicleFormulaTypeDto,string userId);
        void FullShoppingCard(ShoppingVehicleTypeDto shoppingVehicleTypeDto,string userId);
        void DeleteShoppingCard(string userId);
        void Order(string userId);  
        List<Order> getAllOrders();
    }
}
