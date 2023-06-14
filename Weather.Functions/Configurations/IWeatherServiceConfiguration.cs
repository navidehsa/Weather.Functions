
namespace Weather.Functions.Configurations
{
    /// <summary>
    /// Open Weather API Configuration 
    /// </summary>
    public interface IWeatherServiceConfiguration
    {
        /// <summary>
        /// The base url to connect to openWeatherAPI
        /// </summary>
        string ServiceBaseUrl { get; }

        /// <summary>
        /// API key to connect to openWeatherAPI
        /// </summary>
        string ApiKey { get; }
    }
}
