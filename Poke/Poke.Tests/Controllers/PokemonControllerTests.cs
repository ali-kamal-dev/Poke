using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Poke.Controllers;
using Poke.Core;
using Poke.Handlers;
using Poke.Models.ExternalApiResponses;
using Poke.Models.Requests;
using Poke.Models.Responses;
using Poke.Services;
using Poke.Tests.PokeUnitTests.Core;

namespace Poke.Tests.Controllers
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private Mock<IHandler<PokemonInformationRequest, PokemonInformationResponse>> _pokemonInformationHandler;
        private Mock<IHandler<PokemonTranslationRequest, PokemonTranslationResponse>> _translationHandler;

        private PokemonController _controller;

        [SetUp]
        public void Init()
        {
            _translationHandler = new Mock<IHandler<PokemonTranslationRequest, PokemonTranslationResponse>>();
            _pokemonInformationHandler = new Mock<IHandler<PokemonInformationRequest, PokemonInformationResponse>>();

            _controller = new PokemonController(_pokemonInformationHandler.Object, _translationHandler.Object);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Get_Should__Return_OK_If_Pokemon_Found()
        {
            _pokemonInformationHandler.Setup(x =>
                    x.HandleAsync(It.IsAny<PokemonInformationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PokemonInformationResponse() { Status = new Status() { IsResponseSuccessful = true } });

            var result = await _controller.Get("Ditto");
            result.Should().NotBeNull();
            ((ObjectResult)result.Result).StatusCode.Should().Be(200);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Get_Should_Return_NotFound_If_Pokemon_Unrecognized()
        {
            _pokemonInformationHandler.Setup(x =>
                    x.HandleAsync(It.IsAny<PokemonInformationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PokemonInformationResponse() { Status = new Status() { IsResponseSuccessful = false, Msg = "Cant Find Poke." } });

            var result = await _controller.Get("Ditto");
            ((ObjectResult)result.Result).StatusCode.Should().Be(404);
            ((ObjectResult)result.Result).Value.Should().Be("Cant Find Poke.");
        }

        [Test, Category(TestCategories.Unit)]
        public async Task GetTranslated_Should__Return_OK_If_Pokemon_Found()
        {
            _translationHandler.Setup(x =>
                    x.HandleAsync(It.IsAny<PokemonTranslationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PokemonTranslationResponse() { Status = new Status() { IsResponseSuccessful = true } });

            var result = await _controller.Translate("Ditto");
            result.Should().NotBeNull();
            ((ObjectResult)result.Result).StatusCode.Should().Be(200);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task GetTranslated_Should_Return_NotFound_If_Pokemon_Unrecognized()
        {
            _translationHandler.Setup(x =>
                    x.HandleAsync(It.IsAny<PokemonTranslationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PokemonTranslationResponse() { Status = new Status() { IsResponseSuccessful = false, Msg = "Cant Find Poke." } });

            var result = await _controller.Translate("Ditto");
            ((ObjectResult)result.Result).StatusCode.Should().Be(404);
            ((ObjectResult)result.Result).Value.Should().Be("Cant Find Poke.");
        }
    }
}
