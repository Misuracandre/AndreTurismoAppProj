using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;
        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }


        [HttpGet]
        public async Task<List<City>> GetCity()
        {
            return await _cityService.GetCity();
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {

            var newCity = await _cityService.PostCity(city);
            return new ObjectResult(newCity)
            {
                StatusCode = 201
            };
        }
    }
}
