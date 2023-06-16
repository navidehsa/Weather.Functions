using Microsoft.Extensions.Logging;
using Weather.Functions.BaseClient;
using Weather.Functions.Configurations;
using Weather.Functions.Models.ServiceModels;

namespace Weather.Functions.Services
{
    /// <summary>
    /// This Service is responsible to connect to weatherService API
    /// using httpClient to call weather and forecast API
    /// </summary>

    public class WeatherService :  WeatherHttpClientBase, IWeatherService
    {
        public WeatherService(IHttpClientFactory httpClientFactory, IWeatherServiceConfiguration weatherServiceConfiguration,
            ILogger<IWeatherService> logger) : base(httpClientFactory, weatherServiceConfiguration, logger)
        {
        }

        public async Task<CurrentWeatherOutputModel> GetCurrentWeatherAsync(string city)
        {
            ValidateParameters(city);

            var relativePath = $"weather?q={city}&appid={_weatherServiceConfiguration.ApiKey}";

            return await CallWeatherAPIAsync<CurrentWeatherOutputModel>(relativePath);
        }

        public async Task<ForecastsOutputModel> GetForeceastWeatherAsync(string city)
        {
            ValidateParameters(city);

            var relativePath = $"forecast?q={city}&appid={_weatherServiceConfiguration.ApiKey}";

            return await CallWeatherAPIAsync<ForecastsOutputModel>(relativePath);

        }

        private void ValidateParameters(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                ArgumentNullException.ThrowIfNull(city);

            if (string.IsNullOrWhiteSpace(_weatherServiceConfiguration.ApiKey))
                throw new ApplicationException($"{nameof(_weatherServiceConfiguration.ApiKey)} is not configured.");
        }

    }
}
