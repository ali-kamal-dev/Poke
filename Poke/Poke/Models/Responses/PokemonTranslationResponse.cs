using Poke.Core;

namespace Poke.Models.Responses
{
    public class PokemonTranslationResponse : IResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Habitat { get; set; }

        public bool isLegendary { get; set; }

        public Status Status { get; set; } = new Status();
    }
}
