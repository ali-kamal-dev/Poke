using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Poke.Models.ExternalApiResponses
{
    public class BasicPokemonInfo
    {
        [JsonPropertyName("names")]
        public IEnumerable<Names> Names { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public IEnumerable<FlavorText> FlavorTexts { get; set; }

        [JsonPropertyName("habitat")]
        public Habitat Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }
    }

    public class Names
    {
        [JsonPropertyName("language")]
        public Language Language { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }


    public class Habitat
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class FlavorText
    {
        [JsonPropertyName("flavor_text")]
        public string Text { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
