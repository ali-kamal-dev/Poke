using Poke.Core;

namespace Poke.Models.Requests
{
    public class PokemonTranslationRequest : IRequest
    {
        public string Name { get; set; }
    }
}
