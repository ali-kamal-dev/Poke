using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Poke.Core;
using Poke.Models.Requests;
using Poke.Models.Responses;

namespace Poke.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        private readonly IHandler<PokemonInformationRequest, PokemonInformationResponse> _pokemonInformationHandler;
        private readonly IHandler<PokemonTranslationRequest, PokemonTranslationResponse> _translationHandler;

        public PokemonController(ILogger<PokemonController> logger, 
            IHandler<PokemonInformationRequest, PokemonInformationResponse> pokemonInformationHandler,
            IHandler<PokemonTranslationRequest, PokemonTranslationResponse> translationHandler)
        {
            _logger = logger;
            _pokemonInformationHandler = pokemonInformationHandler;
            _translationHandler = translationHandler;
        }

        [HttpGet("{pokemon}")]
        public async Task<ActionResult<PokemonInformationResponse>> Get(string pokemon)
        {

            var response = await _pokemonInformationHandler.HandleAsync(new PokemonInformationRequest()
            {
                Name = pokemon
            });

            if (!response.Status.IsResponseSuccessful)
            {
                return NotFound(response.Status.Msg);
            }

            return response;
        }

        [HttpGet("translated/{pokemon}")]
        public async Task<ActionResult<PokemonTranslationResponse>> Translate(string pokemon)
        {
            var response = await _translationHandler.HandleAsync(new PokemonTranslationRequest()
            {
                Name = pokemon
            });

            if (!response.Status.IsResponseSuccessful)
            {
                return NotFound(response.Status.Msg);
            }

            return response;
        }
    }
}
