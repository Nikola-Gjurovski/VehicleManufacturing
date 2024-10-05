using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;

namespace VehicleReposiotry.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<VehicleApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<VehicleApplicationUser>();
        }
        public IEnumerable<VehicleApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public VehicleApplicationUser Get(string id)
        {
            //return entities
            //   .Include(z => z.ShoppingCart)
            //   .Include("ShoppingCart.ProductShoppingCarts")
            //   //.Include("ShoppingCart.ProductShoppingCarts.Produtct")
            //   .SingleOrDefault(s => s.Id == id);
            return entities
             .Include(u => u.ShoppingCart)
                .ThenInclude(sc => sc.ProductShoppingCarts)
                
            .Include(u => u.Orders)
                .ThenInclude(o => o.ShoppingCart)
                    .ThenInclude(sc => sc.ProductShoppingCarts)
                    
             .SingleOrDefault(u => u.Id == id);
        }
        public void Insert(VehicleApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(VehicleApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(VehicleApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
