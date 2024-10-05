using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;

namespace VehicleReposiotry.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities.Include(z => z.ShoppingCart)
                .Include(z => z.ApplicationUser).Include("ShoppingCart.ProductShoppingCarts").ToList();
        }

        public Order GetOrderDetails(BaseEntity entity)
        {
            return entities.Include(z => z.ShoppingCart)
                .Include(z => z.ApplicationUser).Include("ShoppingCart.ProductShoppingCarts").SingleOrDefaultAsync(z => z.Id == entity.Id).Result;
        }

        public void UpdateOrder(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
   }
