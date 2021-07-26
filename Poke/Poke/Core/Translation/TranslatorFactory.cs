using System.Collections.Generic;
using System.Linq;

namespace Poke.Core.Translation
{
    public class TranslatorFactory : ITranslatorFactory
    {
        private readonly IEnumerable<IValidTranslator> _translators;

        public TranslatorFactory(IEnumerable<IValidTranslator> translators)
        {
            _translators = translators;
        }

        public ITranslator Create(TranslatorEnum translatorType)
        {
            return _translators.Single(item => item.IsValidTranslator(translatorType));
        }

    }
}
