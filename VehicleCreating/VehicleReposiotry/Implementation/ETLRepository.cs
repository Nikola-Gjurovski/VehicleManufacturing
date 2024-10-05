using Domain;
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
  public class ETLRepository:IETLRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Nadvor> entities;
        string errorMessage = string.Empty;

        public ETLRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Nadvor>();
        }
        public IEnumerable<Nadvor> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Nadvor Get(string id)
        {
            //return entities
            //   .Include(z => z.ShoppingCart)
            //   .Include("ShoppingCart.ProductShoppingCarts")
            //   //.Include("ShoppingCart.ProductShoppingCarts.Produtct")
            //   .SingleOrDefault(s => s.Id == id);
            return entities.SingleOrDefault(s => s.Id == id); ;
             
        }
        public void Insert(Nadvor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Nadvor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Nadvor entity)
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
