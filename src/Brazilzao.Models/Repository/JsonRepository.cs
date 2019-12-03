using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Brazilzao.Models.Repository
{
    public class JsonRepository : IRepository
    {
        private readonly string fileBankPath = @"C:\Users\mathv\source\repos\Brazilzao\FileBank";

        private readonly string championshipsPath;

        private readonly List<Championship> championships;

        public JsonRepository()
        {
            this.championshipsPath = Path.Combine(this.fileBankPath, "championships.json");
            this.championships = this.Load();
        }

        private List<Championship> Load()
        {
            if (File.Exists(this.championshipsPath))
            {
                var championshipsContent = File.ReadAllText(this.championshipsPath);
                return JsonConvert.DeserializeObject<List<Championship>>(championshipsContent);
            }
            else
                return new List<Championship>();
        }

        public void Save()
        {
            var championshipsContent = JsonConvert.SerializeObject(this.championships, Formatting.Indented);
            File.WriteAllText(this.championshipsPath, championshipsContent);
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T entity) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public ValueTask<EntityEntry<T>> AddAsync<T>(T entity) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public T Get<T>(int id) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>() where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<T>() where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> GetAsync<T>(int id) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(int id) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            throw new NotImplementedException();
        }
    }
}
