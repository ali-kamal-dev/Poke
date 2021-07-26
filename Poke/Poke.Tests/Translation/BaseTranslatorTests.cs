using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Poke.Core.Translation;
using Poke.Core.Translation.Translators;
using Poke.Tests.PokeUnitTests.Core;

namespace Poke.Tests.Translation
{
    [TestFixture]
    public class BaseTranslatorTests
    {
        private Mock<IHttpClientFactory> _httpClientFactory;
        private ITranslator _translator;

        [SetUp]
        public void Init()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _translator = new YodaTranslator(_httpClientFactory.Object);
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_TextTranslation_Is_Successful_Then_Translation_Is_Returned()
        {
            var translationResponse =
                "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"'Me Translated.\",\"translation\":\"shakespeare\"}}";
            
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Found,
                    Content = new StringContent(translationResponse, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(handler.Object, false);
            client.BaseAddress = new Uri("https://translation.com");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var response = await _translator.Translate("Translate Me", It.IsAny<CancellationToken>());

            response.Should().Be("'Me Translated.");
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_TextTranslation_Fails_Then_Null_Is_Returned()
        {
            var translationResponse =
                "{}";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(translationResponse, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(handler.Object, false);
            client.BaseAddress = new Uri("https://translation.com");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var response = await _translator.Translate("Translate Me", It.IsAny<CancellationToken>());

            response.Should().BeNull();
        }

        [Test, Category(TestCategories.Unit)]
        public async Task Given_TextTranslation_Fails_Then_TranslatedText_Is_Empty()
        {
            var translationResponse =
                "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"\",\"translation\":\"shakespeare\"}}";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Found,
                    Content = new StringContent(translationResponse, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(handler.Object, false);
            client.BaseAddress = new Uri("https://translation.com");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var response = await _translator.Translate("Translate Me", It.IsAny<CancellationToken>());

            response.Should().BeEmpty();
        }


    }
}
