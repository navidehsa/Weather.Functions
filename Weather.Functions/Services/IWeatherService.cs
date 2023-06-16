using Weather.Functions.Models.ServiceModels;

namespace Weather.Functions.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Get Current Weather of specific city 
        /// </summary>
        /// <param name="city">The city name</param>
        /// <returns>Current weather for requested city</returns>
        Task<CurrentWeatherOutputModel> GetCurrentWeatherAsync(string city);

        /// <summary>
        /// Get Foreceast Weather for 5 days
        /// </summary>
        /// <param name="city">The city name</param>
        /// <returns>Hourly forecast for requested city</returns>
        Task<ForecastsOutputModel> GetForeceastWeatherAsync(string city);

    }
}
