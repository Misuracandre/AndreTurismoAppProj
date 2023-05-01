using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.CustomersService.Controllers
{
    [ApiController]
    [Route("weatherforecast/customer")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecastCustomer")]
        public IEnumerable<WeatherForecastCustomer> GetWcustomer()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastCustomer
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}