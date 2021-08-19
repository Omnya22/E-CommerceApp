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

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Users = new GenericRepository<AppUser>(_context);
        }

        public int Done()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
