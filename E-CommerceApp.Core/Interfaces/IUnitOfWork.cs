using E_CommerceApp.Core.Models;
using System;

namespace E_CommerceApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AppUser> Users { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderProduct> OrderProducts { get; }

        bool Commit();
    }
}
