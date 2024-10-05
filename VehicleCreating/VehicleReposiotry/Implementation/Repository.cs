﻿using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReposiotry.Interface;

namespace VehicleReposiotry.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {


        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).IsAssignableFrom(typeof(VehicleParts)))
            {
                return entities.Include("VehicleFormula").AsEnumerable();
            }
            //if (typeof(T).IsAssignableFrom(typeof(Order)))
            //{
            //    return entities
            // .Include(o => o.ApplicationUser)
            // .Include(o => o.ShoppingCart)
            //     .ThenInclude(sc => sc.ProductShoppingCarts)
            // .AsEnumerable();
            //}
            return entities.AsEnumerable();
        }

        public T Get(Guid? id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
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
