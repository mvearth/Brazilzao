using Brazilzao.SDK.Models.Input;
using Brazilzao.SDK.Models.Output;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brazilzao.SDK.Client
{
    public class BrazilzaoService
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly string host;

        public BrazilzaoService(string host = @"http://localhost:50096")
        {
            this.host = host;
        }

        public async Task<IRoundOutputModel> GetRoundByDate(IDateInputModel inputModel)
        {
            var response = await client.GetAsync(
                $"{this.host}/api/Rounds/byDate" +
                    $"?championshipID={inputModel.ChampionshipID}" +
                    $"&day={inputModel.Day}" +
                    $"&month={inputModel.Month}" +
                    $"&year={inputModel.Year}");

            if (response.IsSuccessStatusCode)
            {
                var outputContent = await response.Content.ReadAsStringAsync();

                var output = JsonConvert.DeserializeObject<IRoundOutputModel>(outputContent);

                return output;
            }
            else
                return new RoundOutputModel() { ChampionshipID = inputModel.ChampionshipID, Round = null };
        }

        public async Task<IClassificationOutputModel> GetClassificationByDate(IDateInputModel inputModel)
        {
            var response = await client.GetAsync(
                $"{this.host}/api/Rounds/byDate" +
                    $"?championshipID={inputModel.ChampionshipID}" +
                    $"&day={inputModel.Day}" +
                    $"&month={inputModel.Month}" +
                    $"&year={inputModel.Year}");

            if (response.IsSuccessStatusCode)
            {
                var reponseContent = await response.Content.ReadAsStringAsync();

                var roundOutput = JsonConvert.DeserializeObject<IRoundOutputModel>(reponseContent);

                return new ClassificationOutputModel()
                {
                    ChampionshipID = inputModel.ChampionshipID,
                    RoundID = roundOutput.Round.Id,
                    Classifications = roundOutput.Round.Classifications
                };
            }
            else
                return new ClassificationOutputModel() { ChampionshipID = inputModel.ChampionshipID, Classifications = null };
        }

        public async Task<IRoundOutputModel> UpdateRound(IRoundInputModel inputModel)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(inputModel)))
            {
                var response = await client.PutAsync(
                    $"{this.host}/api/Rounds", content);

                if (response.IsSuccessStatusCode)
                {
                    var outputContent = await response.Content.ReadAsStringAsync();

                    var output = JsonConvert.DeserializeObject<IRoundOutputModel>(outputContent);

                    return output;
                }
                else
                    return new RoundOutputModel() { ChampionshipID = inputModel.ChampionshipID, Round = null };
            }
        }

        public async Task<IRoundsOutputModel> GetRoundsByChampionship(int championshipID)
        {
            var response = await client.GetAsync(
                $"{this.host}/api/Rounds/byChampionship/{championshipID}");

            if (response.IsSuccessStatusCode)
            {
                var outputContent = await response.Content.ReadAsStringAsync();

                var output = JsonConvert.DeserializeObject<IRoundsOutputModel>(outputContent);

                return output;
            }
            else
                return new RoundsOutputModel() { ChampionshipID = championshipID, Rounds = null };
        }
    }
}
