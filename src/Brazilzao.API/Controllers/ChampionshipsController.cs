using System.Threading.Tasks;
using Brazilzao.API.Repository;
using Brazilzao.SDK.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brazilzao.API.Controllers
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
            var Championship = await repository.GetChampionshipAsync(id);

            if (Championship == null)
            {
                return NotFound();
            }

            return Championship;
        }
    }
}