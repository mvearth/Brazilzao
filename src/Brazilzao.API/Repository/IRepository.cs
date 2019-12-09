using Brazilzao.SDK.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brazilzao.API.Repository
{
    public interface IRepository
    {
        Task<Championship> GetChampionshipAsync(int id);

        Task<Round> GetRoundAsync(int id);

        void Update<T>(T entity) where T : class, IEntity;

        Task SaveAsync();
    }
}