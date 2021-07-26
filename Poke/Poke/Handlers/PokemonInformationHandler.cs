using System.Threading;
using System.Threading.Tasks;
using Poke.Core;
using Poke.Models.Requests;
using Poke.Models.Responses;
using Poke.Services;

namespace Poke.Handlers
{
    public class PokemonInformationHandler : IHandler<PokemonInformationRequest, PokemonInformationResponse>
    {
        private readonly IBasicInformationService _basicInformationService;

        public PokemonInformationHandler(IBasicInformationService basicInformationService)
        {
            _basicInformationService = basicInformationService;
        }

        public async ValueTask<PokemonInformationResponse> HandleAsync(PokemonInformationRequest command, CancellationToken token = default)
        {
            var result =  await _basicInformationService.GetFilteredPokemonInfo(command.Name, token);

            var response = new PokemonInformationResponse();

            if (result == null) return response;

            response.Description = result.Description;
            response.Habitat = result.Habitat;
            response.Name = result.Name;
            response.isLegendary = result.IsLegendary;
            response.Status = result.Status;

            return response;
        }
    }
}
