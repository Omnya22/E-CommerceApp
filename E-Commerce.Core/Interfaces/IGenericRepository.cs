using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(Guid id);
    }
}
