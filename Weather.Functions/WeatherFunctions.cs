using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Weather.Functions.Models.ServiceModels;
using Weather.Functions.Services;

namespace Weather.Functions
{
    public class WeatherFunctions
    {
        private readonly IWeatherService _weatherService;

        public WeatherFunctions(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Function("CurrentWeather")]
        [OpenApiOperation("GetCurrentWeather", new[] { "CurrentWeather" })]
        [OpenApiParameter("city", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The city for which to retrieve the current weather")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ForecastsOutputModel), Description = "The forecast weather list for the city")]
        public async Task<HttpResponseData> CurrentWeatherAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            string city = req.Query["city"];
            if (string.IsNullOrWhiteSpace(city))
                return req.CreateResponse(HttpStatusCode.BadRequest);

            var currentWeather = await _weatherService.GetCurrentWeatherAsync(city);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync<CurrentWeatherOutputModel>(currentWeather);
            return response;

        }

        [Function("ForeactsWeather")]
        [OpenApiOperation("GetForeceastWeather", new[] { "ForecastWeather" })]
        [OpenApiParameter("city", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The city for which to retrieve the forecast weather")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ForecastsOutputModel), Description = "The forecast weather list for the city")]

        public async Task<HttpResponseData> GetForeceastWeatherAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {

            string city = req.Query["city"];
            if (string.IsNullOrWhiteSpace(city))
                return req.CreateResponse(HttpStatusCode.BadRequest);

            var forecastWeather = await _weatherService.GetForeceastWeatherAsync(city);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync<ForecastsOutputModel>(forecastWeather);
            return response;

        }
    }
}
