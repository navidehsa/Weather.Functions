# Introduction 

This solution proposes using C#,.NET Core6 to build AzureFunction which expose weather APIs. Azure functions is annotated with Open API and made as dotnet-isolated.
some unit tests are provided using Xunit framework.

## Concepts

- Input models: Both endpoint get `city`(string) as a query parameter 
- Output models
   - CurrentWeatherOutputModel : include `Temp` , `Feels_like`, `Temp_min` and ..
   - ForecastsOutputModel :  include list of forecast weather

## Endpoints

The WeatherFunctions solution Contain two Endpoints with different functonalities:

- [GET] ~/CurrentWeather : Access current weather data for requested city
- [GET] ~/ForeactsWeather : Access to 5 days forecast for requested city

## OpenAPI

Doscumentation is available in swagger UI:
  - ~/swagger/ui

## EnviormentVariables
  - `OPEN_WEATHER_APIKEY` = "api-key you can get from api.openweathermap.org",
  - `OPEN_WEATHER_BASEURL` = "https://api.openweathermap.org/data/2.5/"

Doscumentation is available in swagger UI:
  - ~/swagger/ui


## Step to run

Clone the repository to your local machine.

Install azure-functions-core-tools@4 [Installing](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash)

Navigate to the project directory and open a terminal or command prompt.

Run the command "func start" to start the web API.

Use a tool such as Postman or curl to interact with the API endpoints.




