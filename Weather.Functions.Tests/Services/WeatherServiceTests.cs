
using Microsoft.Extensions.Logging;
using Moq;
using Weather.Functions.Configurations;
using Weather.Functions.Services;

namespace Weather.Functions.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<IWeatherServiceConfiguration> weatherServiceConfigurationMock;
        private readonly Mock<IHttpClientFactory> httpClientFactoryMock;
        private readonly Mock<ILogger<IWeatherService>> loggerMock;

        public WeatherServiceTests()
        {
            weatherServiceConfigurationMock = new Mock<IWeatherServiceConfiguration>();
            httpClientFactoryMock = new Mock<IHttpClientFactory>();
            loggerMock = new Mock<ILogger<IWeatherService>>();
        }


        [Fact]
        public async Task GetCurrentWeatherAsync_NullCity_ThrowsArgumentNullException()
        {
            // Arrange
            var weatherService = new WeatherService(httpClientFactoryMock.Object, weatherServiceConfigurationMock.Object, loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => weatherService.GetCurrentWeatherAsync(null));
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_EmpyApiKey_ThrowsApplicationException()
        {
            // Arrange
            weatherServiceConfigurationMock.Setup(x=>x.ApiKey).Returns(""); // Set an invalid API key
            var weatherService = new WeatherService(httpClientFactoryMock.Object, weatherServiceConfigurationMock.Object, loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => weatherService.GetCurrentWeatherAsync("London"));
        }
    }
}
