using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Core.Translation;
using Poke.Handlers;
using Poke.Models.ExternalApiResponses;
using Poke.Models.Requests;
using Poke.Models.Responses;
using Poke.Services;
using Poke.Tests.PokeUnitTests.Core;

namespace Poke.Tests.Handlers
{
    [TestFixture]
    public class TranslatorFactoryTests
    {
        private Mock<IBasicInformationService> _basicInformationService;
        private Mock<ITranslatorFactory> _translatorFactory;
        private PokemonTranslationHandler _pokemonTranslationHandler;

        [SetUp]
        public void Init()
        {
            _basicInformationService = new Mock<IBasicInformationService>();
            _translatorFactory = new Mock<ITranslatorFactory>();
            _pokemonTranslationHandler = new PokemonTranslationHandler(_basicInformationService.Object, _translatorFactory.Object);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_Does_Not_Exist_Response_Status_IsResponseSuccessful_Should_Be_False()
        {
            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FilteredBasicPokemonInfoResponse());

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Status.IsResponseSuccessful.Should().BeFalse();
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_Lives_In_A_Cave_Then_Yoda_Translation_Is_Applied()
        {
            var yodaTranslation = "Yoda yoda";

            var pokemon = GetValidFilteredPokemonInfoResponse("cave");

            var translator = new Mock<ITranslator>();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(yodaTranslation);

            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            _translatorFactory.Setup(x => x.Create(TranslatorEnum.Yoda)).Returns(translator.Object);

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Description.Should().BeEquivalentTo(yodaTranslation);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_IsLegendary_Then_Yoda_Translation_Is_Applied()
        {
            var yodaTranslation = "Yoda yoda";

            var pokemon = GetValidFilteredPokemonInfoResponse(isLegendary:true);

            var translator = new Mock<ITranslator>();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(yodaTranslation);

            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            _translatorFactory.Setup(x => x.Create(TranslatorEnum.Yoda)).Returns(translator.Object);

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Description.Should().BeEquivalentTo(yodaTranslation);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_DoesNotLiveInCave_or_IsLegendary_Then_Shakespeare_Translation_Is_Applied()
        {
            var shakespeareTranslation = "Shakespeare";

            var pokemon = GetValidFilteredPokemonInfoResponse();

            var translator = new Mock<ITranslator>();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(shakespeareTranslation);

            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            _translatorFactory.Setup(x => x.Create(TranslatorEnum.Shakespeare)).Returns(translator.Object);

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Description.Should().BeEquivalentTo(shakespeareTranslation);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Yoda_Translator_Is_Throttled_Then_No_Translation_Is_Applied()
        {
            var yodaTranslation = (string)null;

            var pokemon = GetValidFilteredPokemonInfoResponse(isLegendary:true);

            var translator = new Mock<ITranslator>();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(yodaTranslation);

            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            _translatorFactory.Setup(x => x.Create(TranslatorEnum.Yoda)).Returns(translator.Object);

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Description.Should().BeEquivalentTo(pokemon.Description);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Shakespeare_Translator_Is_Throttled_Then_No_Translation_Is_Applied()
        {
            var shakespeareTranslation = string.Empty;

            var pokemon = GetValidFilteredPokemonInfoResponse();

            var translator = new Mock<ITranslator>();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(shakespeareTranslation);

            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            _translatorFactory.Setup(x => x.Create(TranslatorEnum.Shakespeare)).Returns(translator.Object);

            var request = new PokemonTranslationRequest();

            var response = await _pokemonTranslationHandler.HandleAsync(request);

            response.Description.Should().BeEquivalentTo(pokemon.Description);
        }

        private FilteredBasicPokemonInfoResponse GetValidFilteredPokemonInfoResponse(string habitat = "urban", bool isLegendary = false)
        {
            return new FilteredBasicPokemonInfoResponse()
            {
                Description = "It can freely recombine its own cellular structure to transform into other life-forms",
                Habitat = habitat,
                IsLegendary = isLegendary,
                Name = "Ditto",
                Status = new Status() { IsResponseSuccessful = true }
            };
        }
    }
}
