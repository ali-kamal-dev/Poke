using System.Net.Http;

namespace Poke.Core.Translation.Translators
{
    public class YodaTranslator : BaseTranslator, IValidTranslator
    {

        public bool IsValidTranslator(TranslatorEnum translatorType)
        {
            return translatorType == TranslatorEnum.Yoda;
        }


        public YodaTranslator(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "yoda")
        {
        }
    }
}
