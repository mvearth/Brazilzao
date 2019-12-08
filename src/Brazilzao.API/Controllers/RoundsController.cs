using Brazilzao.API.Repository;
using Brazilzao.SDK.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Brazilzao.API.Controllers
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

        [HttpGet("byDate")]
        public async Task<ActionResult<Round>> GetRoundByDate(string day, string month, string year)
        {
            var date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));

            var Round = (await this.repository.GetAllAsync<Round>()).FirstOrDefault(r => r.DateTime.Equals(date));

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