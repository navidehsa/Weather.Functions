using Microsoft.Extensions.Logging;
using System.Text.Json;
using Weather.Functions.Configurations;
using Weather.Functions.ExceptionExtensions;
using Weather.Functions.Services;

namespace Weather.Functions.BaseClient
{
    public abstract class WeatherHttpClientBase
    {

        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IWeatherServiceConfiguration _weatherServiceConfiguration;
        protected readonly ILogger<IWeatherService> _logger;
        protected WeatherHttpClientBase(IHttpClientFactory httpClientFactory, IWeatherServiceConfiguration weatherServiceConfiguration, ILogger<IWeatherService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _weatherServiceConfiguration = weatherServiceConfiguration;
            _logger = logger;
        }

        protected internal async Task<TResponse> CallWeatherAPIAsync<TResponse>(string relativePath)
        {

            string combinedUrl = Path.Combine(_weatherServiceConfiguration.ServiceBaseUrl, relativePath);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(combinedUrl),
                Method = HttpMethod.Get
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponse = await httpClient.SendAsync(request);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                await ThrowErrorAsync(httpResponse, combinedUrl);
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<TResponse>(responseString, options);
            return response;

        }

        protected internal async Task ThrowErrorAsync(HttpResponseMessage httpResponse, string path)
        {
            var response = string.Empty;
            if (httpResponse.Content != null)
            {
                response = (await httpResponse.Content.ReadAsStringAsync());
            }

            var errorMsg =
                $"Error during call to {path}. Response: {(int)httpResponse.StatusCode} {httpResponse.ReasonPhrase}. Response: {response} ";

            _logger.LogError(errorMsg);
            throw new ServiceException(httpResponse, errorMsg);
        }
    }
}
