using Poke.Core;

namespace Poke.Models.Requests
{
    public class PokemonInformationRequest : IRequest
    {
        public string Name { get; set; }
    }
}
