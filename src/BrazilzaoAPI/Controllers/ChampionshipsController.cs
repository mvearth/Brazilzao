using System.Threading.Tasks;
using Brazilzao.Models;
using Brazilzao.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brazilzao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionshipsController : ControllerBase
    {
        private readonly IRepository repository;

        public ChampionshipsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Championships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Championship>> GetChampionship(int id)
        {
            var Championship = await repository.GetAsync<Championship>(id);

            if (Championship == null)
            {
                return NotFound();
            }

            return Championship;
        }

        // POST: api/Championships
        [HttpPost]
        public async Task AddChampionship(Championship championship)
        {
            await this.repository.AddAsync(championship);
            await this.repository.SaveAsync();
        }

        // PUT: api/Championships/5
        [HttpPut("{id}")]
        public async Task UpdateChampionship(int id, Championship championship)
        {
            var championshipToUpdate = await this.repository.GetAsync<Championship>(id);

            championshipToUpdate.Edition = championshipToUpdate.Edition != championship.Edition
                ? championship.Edition : championshipToUpdate.Edition;  
            championshipToUpdate.Id = championshipToUpdate.Id != championship.Id ? championship.Id : championshipToUpdate.Id;  
            championshipToUpdate.InitialDate = championshipToUpdate.InitialDate != championship.InitialDate 
                ? championship.InitialDate : championshipToUpdate.InitialDate;  
            championshipToUpdate.Name = championshipToUpdate.Name != championship.Name ? championship.Name : championshipToUpdate.Name;  

            this.repository.Update(championshipToUpdate);
            await this.repository.SaveAsync();
        }
    }
}