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
    }
}