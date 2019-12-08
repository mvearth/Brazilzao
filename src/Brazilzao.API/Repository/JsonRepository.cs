using Brazilzao.SDK.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Brazilzao.API.Repository
{
    public class JsonRepository : IRepository
    {
        private readonly string championshipsPath = @"C:\Users\mathv\source\repos\Brazilzao\Championships";
        private readonly string teamsPath = @"C:\Users\mathv\source\repos\Brazilzao\Teams\teams.json";

        private readonly List<Championship> championships;
        private readonly List<Team> teams;

        public JsonRepository()
        {
            this.championships = new List<Championship>();
            this.teams = new List<Team>();

            this.LoadChampionships();
            this.LoadTeams();
        }

        private void LoadChampionships()
        {
            if (Directory.Exists(this.championshipsPath))
            {
                foreach (var file in Directory.GetFiles(this.championshipsPath))
                {
                    var championshipsContent = File.ReadAllText(file);
                    this.championships.Add(JsonConvert.DeserializeObject<Championship>(championshipsContent));
                }
            }
            else
            {
                Directory.CreateDirectory(this.championshipsPath);
            }
        }

        private void LoadTeams()
        {
            if (File.Exists(this.teamsPath))
            {
                var teamContent = File.ReadAllText(this.teamsPath);
                this.teams.Clear();
                this.teams.AddRange(JsonConvert.DeserializeObject<List<Team>>(teamContent));
            }
            else
            {
                Directory.CreateDirectory(this.championshipsPath);
            }
        }

        public void Save()
        {
            foreach (var championship in this.championships)
            {
                var championshipsContent = JsonConvert.SerializeObject(championship, Formatting.Indented);

                File.WriteAllText(
                    Path.Combine(this.championshipsPath, $"{championship.Name}_{championship.Edition}.json"),
                        championshipsContent);
            }

            var teamsContent = JsonConvert.SerializeObject(this.teams, Formatting.Indented);

            File.WriteAllText(
                    this.teamsPath,
                        teamsContent);

        }

        public Task SaveAsync()
        {
            return new Task(() => this.Save());
        }

        public void Add<T>(T entity) where T : class, IEntity
        {
            if (entity is Championship championship)
                this.championships.Add(championship);
            else if (entity is Team team)
                this.teams.Add(team);
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
