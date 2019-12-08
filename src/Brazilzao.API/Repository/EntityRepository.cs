using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brazilzao.API.Contexts;
using Brazilzao.SDK.Models;
using Microsoft.EntityFrameworkCore;

namespace Brazilzao.API.Repository
{
    public class EntityRepository : IRepository
    {
        private BrazilzaoContext context;

        public EntityRepository(BrazilzaoContext context) => this.context = context;

        public void Save() =>
            this.context.SaveChanges();

        public Task SaveAsync() => 
            this.context.SaveChangesAsync();

        public void Add<T>(T entity) where T : class, IEntity =>
            this.context.Set<T>().Add(entity);

        public ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> AddAsync<T>(T entity) where T : class, IEntity =>
            this.context.Set<T>().AddAsync(entity);

        public T Get<T>(int id) where T : class, IEntity =>
            this.context.Set<T>().Find(id);

        public ValueTask<T> GetAsync<T>(int id) where T : class, IEntity =>
            this.context.Set<T>().FindAsync(id);

        public IList<T> GetAll<T>() where T : class, IEntity =>
            this.context.Set<T>().ToList();

        public Task<List<T>> GetAllAsync<T>() where T : class, IEntity =>
            this.context.Set<T>().ToListAsync();

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