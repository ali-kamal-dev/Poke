using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Poke.Core;
using Poke.Models.Requests;
using Poke.Models.Responses;

namespace Poke.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
 
        private readonly IHandler<PokemonInformationRequest, PokemonInformationResponse> _pokemonInformationHandler;
        private readonly IHandler<PokemonTranslationRequest, PokemonTranslationResponse> _translationHandler;

        public PokemonController(
            IHandler<PokemonInformationRequest, PokemonInformationResponse> pokemonInformationHandler,
            IHandler<PokemonTranslationRequest, PokemonTranslationResponse> translationHandler)
        {
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

            return Ok(response);
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

            return Ok(response);
        }
    }
}
