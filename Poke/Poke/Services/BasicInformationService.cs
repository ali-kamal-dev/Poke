using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Poke.Models.ExternalApiResponses;

namespace Poke.Services
{
    public class BasicInformationService : IBasicInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasicInformationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async ValueTask<FilteredBasicPokemonInfoResponse> GetFilteredPokemonInfo(string name, CancellationToken token = default)
        {
            var pokemonInfo = await GetAllPokemonInfo(name, token);

            var filteredResult = FilterResult(pokemonInfo.Pokemon, "en");

            filteredResult.Status = pokemonInfo.Status;

            return filteredResult;
        }

        private async ValueTask<BasicPokemonInfoResponse> GetAllPokemonInfo(string name,
            CancellationToken token = default)
        {
            var response = new BasicPokemonInfoResponse();

            var httpClient = _httpClientFactory.CreateClient("pokemonClient");

            var apiResponse = await httpClient.GetAsync($"/api/v2/pokemon-species/{name}", token);

            if (!apiResponse.IsSuccessStatusCode)
            {
                response.Status.Msg = $"No pokemon with name: {name} could be found";
                return response;
            }

            var jsonResponse = await apiResponse.Content.ReadAsStringAsync();

            response.Status.IsResponseSuccessful = true;
            var basicPokemonInfoResponse = JsonSerializer.Deserialize<BasicPokemonInfo>(jsonResponse);
            response.Pokemon = basicPokemonInfoResponse;

            return response;
        }

        private FilteredBasicPokemonInfoResponse FilterResult(BasicPokemonInfo basicPokemonInfo, string lang)
        {
            var response = new FilteredBasicPokemonInfoResponse();

            if (basicPokemonInfo != null)
            {
                response.Name = basicPokemonInfo.Names.FirstOrDefault(x => x?.Language?.Name == lang)?.Name;
                response.Description =
                    basicPokemonInfo.FlavorTexts.FirstOrDefault(x => x?.Language?.Name == lang)?.Text;
                response.IsLegendary = basicPokemonInfo.IsLegendary;
                response.Habitat = basicPokemonInfo.Habitat.Name;
            }

            return response;
        }
    }
}
