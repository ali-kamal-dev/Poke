using Poke.Models.Responses;

namespace Poke.Models.ExternalApiResponses
{
    public class BasicPokemonInfoResponse
    {
        public Status Status { get; set; } = new Status();
        public BasicPokemonInfo Pokemon { get; set; }
    }
}
