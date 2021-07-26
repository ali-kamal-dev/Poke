using System.Threading;
using System.Threading.Tasks;
using Poke.Core;
using Poke.Core.Translation;
using Poke.Models.Requests;
using Poke.Models.Responses;
using Poke.Services;

namespace Poke.Handlers
{
    public class PokemonTranslationHandler : IHandler<PokemonTranslationRequest, PokemonTranslationResponse>
    {
        private readonly IBasicInformationService _basicInformationService;
        private readonly ITranslatorFactory _translatorFactory;

        private const string CaveHabitat = "cave";

        public PokemonTranslationHandler(IBasicInformationService basicInformationService, ITranslatorFactory translatorFactory)
        {
            _basicInformationService = basicInformationService;
            _translatorFactory = translatorFactory;
        }

        public async ValueTask<PokemonTranslationResponse> HandleAsync(PokemonTranslationRequest command, CancellationToken token = default)
        {
            var result =  await _basicInformationService.GetFilteredPokemonInfo(command.Name, token);

            var response = new PokemonTranslationResponse();

            if (!result.Status.IsResponseSuccessful)
            {
                return response;
            }

            response.Status = result.Status;
            var translatedDescription = string.Empty;

            if (result.Habitat.ToLower() == CaveHabitat || result.IsLegendary)
            {
                translatedDescription = await (_translatorFactory.Create(TranslatorEnum.Yoda)).Translate(result.Description, token);
            }
            else
            {
                translatedDescription = await (_translatorFactory.Create(TranslatorEnum.Shakespeare)).Translate(result.Description, token);
            }

            response.Description = !string.IsNullOrEmpty(translatedDescription) ? translatedDescription : result.Description;
            response.Habitat = result.Habitat;
            response.Name = result.Name;
            response.isLegendary = result.IsLegendary;

            return response;
        }
    }
}
