using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Poke.Models.ExternalApiResponses;

namespace Poke.Core.Translation
{
    public abstract class BaseTranslator : IBaseTranslator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _route;

        protected BaseTranslator(IHttpClientFactory httpClientFactory, string route)
        {
            _httpClientFactory = httpClientFactory;
            _route = route;
        }

        public async Task<string> Translate(string text, CancellationToken token = default)
        {
            var httpClient = _httpClientFactory.CreateClient("translatorClient");
            
            var values = new Dictionary<string, string>();
            values.Add("text", text);
            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync($"{_route}.json", content, token);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var translation = JsonSerializer.Deserialize<TranslationContentsResponse>(jsonResponse);

            return translation?.Contents?.Translated;
        }
    }
}
