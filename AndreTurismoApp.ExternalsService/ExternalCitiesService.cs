using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Serialization;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalsService
{
    public class ExternalCitiesService
    {
        static readonly HttpClient cities = new HttpClient();

        public async Task<List<City>> GetCity()
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:7008/api/Cities");
                response.EnsureSuccessStatusCode();
                string citiesJson = await response.Content.ReadAsStringAsync();
                var citiesList = JsonConvert.DeserializeObject<List<City>>(citiesJson);
                return citiesList;
            }
            catch (HttpRequestException e)
            {
                return new List<City>();
            }
        }

        public async Task<City> GetCityById(int id)
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:7008/api/Cities/" + id);
                response.EnsureSuccessStatusCode();
                string citiesJson = await response.Content.ReadAsStringAsync();
                var citiesList = JsonConvert.DeserializeObject<City>(citiesJson);
                return citiesList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostCity(City city)
        {
            HttpResponseMessage response = await cities.PostAsJsonAsync("https://localhost:7008/api/Cities", city);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutCity(City city)
        {
            HttpResponseMessage response = await cities.PutAsJsonAsync("https://localhost:7008/api/Cities", city);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCity(int id)
        {
            HttpResponseMessage response = await cities.DeleteAsync("https://localhost:7008/api/Cities/" + id);
            return response.StatusCode;
        }
    }
}