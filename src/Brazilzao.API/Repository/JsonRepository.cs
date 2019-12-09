using Brazilzao.SDK.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Brazilzao.API.Repository
{
    public class JsonRepository : IRepository
    {
        private readonly string championshipsPath = @"C:\Users\mathv\source\repos\Brazilzao\Championships";

        private readonly List<Championship> championships;

        public JsonRepository()
        {
            this.championships = new List<Championship>();

            this.LoadChampionships();
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

        public void Save()
        {
            foreach (var championship in this.championships)
            {
                var championshipsContent = JsonConvert.SerializeObject(championship, Formatting.Indented);

                File.WriteAllText(
                    Path.Combine(this.championshipsPath, $"{championship.Name}_{championship.Edition}.json"),
                        championshipsContent);
            }
        }

        public Task SaveAsync()
        {
            return new Task(() => this.Save());
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            if(entity is Championship championship)
            {
                var championshipIndex = this.championships.IndexOf(championship);
                this.championships[championshipIndex] = championship;
            }
        }

        public Task<Championship> GetChampionshipAsync(int id)
        {
            return Task.Run(() => this.championships.FirstOrDefault(c => c.Id.Equals(id)));
        }

        public Task<Round> GetRoundAsync(int id)
        {
            return new Task<Round>(() =>
            {
                foreach (var championship in this.championships)
                {
                    var round = championship.Rounds.FirstOrDefault(r => r.Id.Equals(id));

                    if (round != null)
                        return round;
                }

                return null;
            });
        }
    }
}
