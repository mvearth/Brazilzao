using System.Threading.Tasks;
using Brazilzao.Models;
using Brazilzao.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brazilzao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly IRepository repository;

        public RoundsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Rounds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Round>> GetRound(int id)
        {
            var Round = await repository.GetAsync<Round>(id);

            if (Round == null)
            {
                return NotFound();
            }

            return Round;
        }

        // POST: api/Rounds
        [HttpPost]
        public async Task AddRound(Round Round)
        {
            await this.repository.AddAsync(Round);
            await this.repository.SaveAsync();
        }
    }
}