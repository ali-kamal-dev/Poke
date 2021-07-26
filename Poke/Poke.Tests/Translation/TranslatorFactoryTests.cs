using System.Collections.Generic;
using System.Net.Http;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Core.Translation;
using Poke.Core.Translation.Translators;
using Poke.Tests.PokeUnitTests.Core;

namespace Poke.Tests.Translation
{
    [TestFixture]
    public class TranslatorFactoryTests
    {
        private IEnumerable<IValidTranslator> _translators;
        private TranslatorFactory _translatorFactory;

        [SetUp]
        public void Init()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();
            _translators = new List<IValidTranslator>(){new ShakespeareTranslator(httpClientFactory.Object), new YodaTranslator(httpClientFactory.Object) };
            _translatorFactory = new TranslatorFactory(_translators);
        }

        [Test, Category(TestCategories.Unit)]
        public void Given_ShakespeareTranslator_Is_Required_Then_Shakespeare_TranslatorIsCreated()
        {
            var translator = _translatorFactory.Create(TranslatorEnum.Shakespeare);

            translator.Should().BeOfType<ShakespeareTranslator>();
        }

        [Test, Category(TestCategories.Unit)]
        public void Given_YodaTranslator_Is_Required_Then_Yoda_TranslatorIsCreated()
        {
            var translator = _translatorFactory.Create(TranslatorEnum.Yoda);

            translator.Should().BeOfType<YodaTranslator>();
        }
    }
}
