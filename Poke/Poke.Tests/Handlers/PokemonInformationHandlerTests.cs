using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Handlers;
using Poke.Models.ExternalApiResponses;
using Poke.Models.Requests;
using Poke.Models.Responses;
using Poke.Services;
using Poke.Tests.PokeUnitTests.Core;

namespace Poke.Tests.Handlers
{
    [TestFixture]
    public class PokemonInformationHandlerTests
    {
        private Mock<IBasicInformationService> _basicInformationService;
        private PokemonInformationHandler _pokemonInformationHandler;

        [SetUp]
        public void Init()
        {
            _basicInformationService = new Mock<IBasicInformationService>();
            _pokemonInformationHandler = new PokemonInformationHandler(_basicInformationService.Object);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Should_Call_BasicInformationService_Once()
        {
            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetValidFilteredPokemonInfoResponse());

            var request = new PokemonInformationRequest();

            await _pokemonInformationHandler.HandleAsync(request);

            _basicInformationService.Verify(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_Is_Retrieved_Status_IsResponseSuccessful_Should_Be_True()
        {
            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetValidFilteredPokemonInfoResponse());

            var request = new PokemonInformationRequest();

            var response = await _pokemonInformationHandler.HandleAsync(request);

            response.Status.IsResponseSuccessful.Should().BeTrue();
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_Pokemon_Does_Not_Exist_Response_Status_IsResponseSuccessful_Should_Be_False()
        {
            _basicInformationService.Setup(x => x.GetFilteredPokemonInfo(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((FilteredBasicPokemonInfoResponse) null);

            var request = new PokemonInformationRequest();

            var response = await _pokemonInformationHandler.HandleAsync(request);

            response.Status.IsResponseSuccessful.Should().BeFalse();
        }

        private FilteredBasicPokemonInfoResponse GetValidFilteredPokemonInfoResponse()
        {
            return new FilteredBasicPokemonInfoResponse()
            {
                Description = "It can freely recombine its own cellular structure to transform into other life-forms",
                Habitat = "urban",
                IsLegendary = false,
                Name = "Ditto",
                Status = new Status() {IsResponseSuccessful = true}
            };
        }
    }
}
