using Brazilzao.SDK.Models.Input;
using Brazilzao.SDK.Models.Output;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Brazilzao.SDK.Client
{
    public class BrazilzaoService
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly string host;

        public BrazilzaoService(string host = @"http://localhost:64343")
        {
            this.host = host;
        }

        public async Task<RoundOutputModel> GetRoundByDate(IDateInputModel inputModel)
        {
            var response = client.GetAsync(
                $"{this.host}/api/Rounds/byDate" +
                    $"?championshipID={inputModel.ChampionshipID}" +
                    $"&day={inputModel.Day}" +
                    $"&month={inputModel.Month}" +
                    $"&year={inputModel.Year}").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var outputContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var output = JsonConvert.DeserializeObject<RoundOutputModel>(outputContent);

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
                var reponseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var roundOutput = JsonConvert.DeserializeObject<RoundOutputModel>(reponseContent);

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

        public async Task<RoundOutputModel> UpdateRound(IRoundInputModel inputModel)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json"))
            {
                var response = await client.PutAsync(
                    $"{this.host}/api/Rounds", content);

                if (response.IsSuccessStatusCode)
                {
                    return new RoundOutputModel() { ChampionshipID = inputModel.ChampionshipID, Round = inputModel.Round };
                }
                else
                    return new RoundOutputModel() { ChampionshipID = inputModel.ChampionshipID, Round = null };
            }
        }

        public async Task<RoundsOutputModel> GetRoundsByChampionship(int championshipID)
        {
            var response = client.GetAsync(
                $"{this.host}/api/Rounds/byChampionship/{championshipID}").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var outputContent = await response.Content.ReadAsStringAsync();

                var output = JsonConvert.DeserializeObject<RoundsOutputModel>(outputContent);

                return output;
            }
            else
                return new RoundsOutputModel() { ChampionshipID = championshipID, Rounds = null };
        }
    }
}
