using Microsoft.Extensions.Configuration;

namespace Weather.Functions.Configurations
{
    public class WeatherServiceConfiguration : IWeatherServiceConfiguration
    {
        private readonly IConfiguration _configuration;

        public WeatherServiceConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ServiceBaseUrl => _configuration["OPEN_WEATHER_BASEURL"] ?? "https://api.openweathermap.org/data/2.5/";


        public string ApiKey => _configuration["OPEN_WEATHER_APIKEY"] ?? string.Empty;
    }
}
