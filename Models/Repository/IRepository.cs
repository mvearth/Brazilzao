using System.Collections.Generic;

namespace Brazilzao.Models.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class, IEntity;

        T Get<T>(int id) where T : class, IEntity;

        void Update<T>(T entity) where T : class, IEntity;

        void Remove<T>(int id) where T : class, IEntity;

        IList<T> GetAll<T>() where T : class, IEntity;

        void Save();
    }
}