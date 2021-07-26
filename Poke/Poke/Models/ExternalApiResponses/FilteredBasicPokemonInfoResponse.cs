using Poke.Models.Responses;

namespace Poke.Models.ExternalApiResponses
{
    public class FilteredBasicPokemonInfoResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Habitat { get; set; }

        public bool IsLegendary { get; set; }

        public Status Status { get; set; } = new Status();
    }
}
