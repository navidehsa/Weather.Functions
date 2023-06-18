using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weather.Functions.Configurations;
using Weather.Functions.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureOpenApi()
    .ConfigureServices(_ =>
       {
           _.AddHttpClient();
           _.AddSingleton<IWeatherService, WeatherService>();
           _.AddSingleton<IWeatherServiceConfiguration, WeatherServiceConfiguration>();
           _.AddLogging();
       })
    .Build();

host.Run();
