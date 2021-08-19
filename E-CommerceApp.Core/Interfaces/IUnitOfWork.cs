using E_CommerceApp.Core.Models;
using System;

namespace E_CommerceApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AppUser> Users { get; }

        int Done();
    }
}
