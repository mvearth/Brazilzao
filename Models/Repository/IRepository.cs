using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brazilzao.Models.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class, IEntity;

        T Get<T>(int id) where T : class, IEntity;

        void Update<T>(T entity) where T : class, IEntity;

        void Remove<T>(int id) where T : class, IEntity;

        void Save();

        IList<T> GetAll<T>() where T : class, IEntity;

        ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> AddAsync<T>(T entity) where T : class, IEntity;

        ValueTask<T> GetAsync<T>(int id) where T : class, IEntity;

        Task<List<T>> GetAllAsync<T>() where T : class, IEntity;

        Task SaveAsync();
    }
}