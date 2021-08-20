using E_CommerceApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.EF.DataAccess
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<OrderProduct>()
                .HasKey(t => new { t.ProductId, t.OrderId });

            builder.Entity<OrderProduct>()
                .HasOne(b => b.Order)
                .WithMany(ba => ba.OrderProducts)
                .HasForeignKey(bi => bi.OrderId);

            builder.Entity<OrderProduct>()
              .HasOne(b => b.Product)
              .WithMany(ba => ba.OrderProducts)
              .HasForeignKey(bi => bi.ProductId);

            base.OnModelCreating(builder);
        }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

    }
}
