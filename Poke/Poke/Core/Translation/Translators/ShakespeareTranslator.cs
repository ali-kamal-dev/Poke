using System.Net.Http;

namespace Poke.Core.Translation.Translators
{
    public class ShakespeareTranslator : BaseTranslator, IValidTranslator
    {
        public bool IsValidTranslator(TranslatorEnum translatorType)
        {
            return translatorType == TranslatorEnum.Shakespeare;
        }

        public ShakespeareTranslator(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "shakespeare")
        {
        }
    }
}
