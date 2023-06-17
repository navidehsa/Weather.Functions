using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using System.Net;
using System.Security.Claims;
using Weather.Functions.Services;

namespace Weather.Functions.Tests
{
    public partial class WeatherFunctionsTests
    {
        private readonly WeatherFunctions weatherFunctions;
        private readonly Mock<IWeatherService> weatherServiceMock;

        public WeatherFunctionsTests()
        {
            weatherServiceMock = new Mock<IWeatherService>();
            weatherFunctions = new WeatherFunctions(weatherServiceMock.Object);
        }

        [Fact]
        public async Task CurrentWeatherAsync_ReturnsBadRequest_WhenCityIsMissing()
        {
            // Arrange
            var context = new Mock<FunctionContext>();
            var body = Stream.Null;
            var httpRequestDataMock = new FakeHttpRequestData(
                context.Object,
                new Uri("https://test.com"),
                body);
            httpRequestDataMock.Query.Add("city", "");

            // Act
            var response = await weatherFunctions.CurrentWeatherAsync(httpRequestDataMock);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        public class FakeHttpRequestData : HttpRequestData
        {
            public FakeHttpRequestData(FunctionContext functionContext, Uri url, Stream body = null) : base(functionContext)
            {
                Url = url;
                Body = body ?? new MemoryStream();
            }

            public override Stream Body { get; } = new MemoryStream();

            public override HttpHeadersCollection Headers { get; } = new HttpHeadersCollection();

            public override IReadOnlyCollection<IHttpCookie> Cookies { get; }

            public override Uri Url { get; }

            public override IEnumerable<ClaimsIdentity> Identities { get; }

            public override string Method { get; }

            public override HttpResponseData CreateResponse()
            {
                return new FakeHttpResponseData(FunctionContext);
            }
        }

        public class FakeHttpResponseData : HttpResponseData
        {
            public FakeHttpResponseData(FunctionContext functionContext) : base(functionContext)
            {
            }

            public override HttpStatusCode StatusCode { get; set; }
            public override HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();
            public override Stream Body { get; set; } = new MemoryStream();
            public override HttpCookies Cookies { get; }
        }


    }



}
