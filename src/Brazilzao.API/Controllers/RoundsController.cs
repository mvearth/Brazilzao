using Brazilzao.API.Repository;
using Brazilzao.SDK.Models;
using Brazilzao.SDK.Models.Input;
using Brazilzao.SDK.Models.Output;
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
            var Round = await repository.GetRoundAsync(id);

            if (Round == null)
            {
                return NotFound();
            }

            return Round;
        }

        [HttpGet("byChampionship/{id}")]
        public async Task<ActionResult<RoundsOutputModel>> GetRoundsByChampionship(int id)
        {
            var championship = await repository.GetChampionshipAsync(id);

            if (championship?.Rounds is null)
            {
                return NotFound();
            }

            return new RoundsOutputModel() { Rounds = championship.Rounds, ChampionshipID = id };
        }

        [HttpGet("byDate")]
        public async Task<ActionResult<RoundOutputModel>> GetRoundByDate(string championshipID, string day, string month, string year)
        {
            var date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));

            if (int.TryParse(championshipID, out int championshipIntID))
            {
                var championship = await this.repository.GetChampionshipAsync(championshipIntID);

                var Round = championship.Rounds.OrderBy(r => r.DateTime.Subtract(date)).FirstOrDefault();

                if (Round == null)
                    return NotFound();

                return new RoundOutputModel() { ChampionshipID = championshipIntID, Round = Round };
            }
            else
                return NotFound();
        }

        [HttpPut]
        public async Task UpdateRound(RoundInputModel inputModel)
        {
            if (inputModel?.Round != null)
            {
                var championship = await this.repository.GetChampionshipAsync(inputModel.ChampionshipID);

                var round = championship.Rounds.FirstOrDefault(r => r.Id.Equals(inputModel.Round.Id));

                var roundIndex = championship.Rounds.IndexOf(round);

                var previousRound = championship.Rounds.FirstOrDefault(r => round.DateTime.Subtract(r.DateTime).Days < 5 && round.DateTime.Subtract(r.DateTime).Days > 0);

                if (previousRound != null)
                {
                    foreach (var classification in previousRound.Classifications)
                    {
                        var currentClassification = inputModel.Round.Classifications.FirstOrDefault(c => c.Team.Id.Equals(classification.Team.Id));

                        currentClassification.GoalsAgainst = classification.GoalsAgainst;
                        currentClassification.GoalsFor = classification.GoalsFor;
                        currentClassification.Loses = classification.Loses;
                        currentClassification.Points = classification.Points;
                        currentClassification.Wins = classification.Wins;
                    }
                    inputModel.Round.Classifications = previousRound.Classifications;
                }

                inputModel.Round.DistributePoints();

                championship.Rounds[roundIndex] = inputModel.Round;

                this.repository.Update(championship);

                this.repository.Update(round);

                foreach (var teamClassification in round.Classifications)
                    this.repository.Update(teamClassification);

                await this.repository.SaveAsync();
            }
        }
    }
}