using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Brazilzao.Models.Repository{
    public class EntityRepository : IRepository
    {
        private DbContext context;

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Add<T>(T entity) where T : class, IEntity
        {
            this.context.Set<T>().Add(entity);
        }

        public T Get<T>(int id) where T : class, IEntity
        {
            return this.context.Set<T>().Find(id);
        }

        public IList<T> GetAll<T>() where T : class, IEntity
        {
            return this.context.Set<T>().ToList();
        }

        public void Remove<T>(int id) where T : class, IEntity
        {
            var entity = this.Get<T>(id);
            this.context.Set<T>().Remove(entity);
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }
    }
}