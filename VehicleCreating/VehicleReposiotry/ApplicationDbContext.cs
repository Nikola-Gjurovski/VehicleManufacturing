using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace VehicleReposiotry
{
    public class ApplicationDbContext: IdentityDbContext<VehicleApplicationUser>
    {
        public virtual DbSet<VehicleFormula> VehicleFormulas { get; set; }
        public virtual DbSet<VehicleParts> VehicleParts { get; set; }
        public virtual DbSet<ShoppingCart>ShoppingCarts { get; set; }
        public virtual DbSet<Order>Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        public virtual DbSet<Nadvor> Nadvors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
           
        }
    }
}
