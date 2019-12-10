using Brazilzao.API.Contexts;
using Brazilzao.SDK.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Brazilzao.API.Repository
{
    public class EntityRepository : IRepository
    {
        private BrazilzaoContext context;

        public EntityRepository(BrazilzaoContext context) => this.context = context;

        public Task SaveAsync() =>
            this.context.SaveChangesAsync();

        public T Get<T>(int id) where T : class, IEntity =>
            this.context.Set<T>().Find(id);

        public void Remove<T>(int id) where T : class, IEntity
        {
            var entity = this.Get<T>(id);
            this.context.Set<T>().Remove(entity);
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            this.context.Set<T>().Update(entity);

            this.context.SaveChanges();
        }

        public Task<Championship> GetChampionshipAsync(int id)
        {
            return this.context.Championships
                .Include("Rounds")
                .Include("Rounds.Classifications")
                .Include("Rounds.Classifications.Team")
                .Include("Rounds.Matches")
                .Include("Rounds.Matches.Home")
                .Include("Rounds.Matches.Visitor")
                .FirstAsync(c => c.Id.Equals(id));
        }

        public Task<Round> GetRoundAsync(int id)
        {
            return this.context.Rounds
                .Include("Classifications")
                .Include("Classifications.Team")
                .Include("Matches")
                .Include("Matches.Home")
                .Include("Matches.Visitor")
                .FirstAsync(r => r.Id.Equals(id));
        }
    }
}