using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.AddressesService.Controllers
{
    [ApiController]
    [Route("weatherforecast/address")]
    public class WeatherForecastAddressController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastAddressController> _logger;

        public WeatherForecastAddressController(ILogger<WeatherForecastAddressController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecastAddress>), StatusCodes.Status200OK,
        Type = typeof(IEnumerable<WeatherForecastAddress>))] // adicionando o tipo do esquema
        public IEnumerable<WeatherForecastAddress> GetWaddress()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastAddress
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}