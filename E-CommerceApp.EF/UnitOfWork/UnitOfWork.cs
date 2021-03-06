using E_CommerceApp.Core.Interfaces;
using E_CommerceApp.Core.Models;
using E_CommerceApp.EF.DataAccess;
using E_CommerceApp.EF.Repository;

namespace E_CommerceApp.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<AppUser> Users { get; set; }

        public IGenericRepository<Product> Products { get; set; }

        public IGenericRepository<Order> Orders { get; set; }
        
        public IGenericRepository<OrderProduct> OrderProducts { get; set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Users = new GenericRepository<AppUser>(_context);
            Orders = new GenericRepository<Order>(_context);
            Products = new GenericRepository<Product>(_context);
            OrderProducts = new GenericRepository<OrderProduct>(_context);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
