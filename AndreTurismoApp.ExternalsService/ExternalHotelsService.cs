using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalsService
{
    public class ExternalHotelsService
    {
        static readonly HttpClient hotels = new HttpClient();

        public async Task<List<Hotel>> GetHotel()
        {
            try
            {
                HttpResponseMessage response = await hotels.GetAsync("https://localhost:8080/api/Hotels");
                response.EnsureSuccessStatusCode();
                string hotelsJson = await response.Content.ReadAsStringAsync();
                var hotelsList = JsonConvert.DeserializeObject<List<Hotel>>(hotelsJson);
                return hotelsList;
            }
            catch (HttpRequestException e)
            {
                return new List<Hotel>();
            }
        }
        public async Task<Hotel> GetHotelById(int id)
        {
            try
            {
                HttpResponseMessage response = await hotels.GetAsync("https://localhost:8080/api/Hotels" + id);
                response.EnsureSuccessStatusCode();
                string hotelsJson = await response.Content.ReadAsStringAsync();
                var hotelsList = JsonConvert.DeserializeObject<Hotel>(hotelsJson);
                return hotelsList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostHotel(Hotel hotel)
        {
            HttpResponseMessage response = await hotels.PostAsJsonAsync("https://localhost:8080/api/Hotels", hotel);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutHotel(Hotel hotel)
        {
            HttpResponseMessage response = await hotels.PutAsJsonAsync("https://localhost:8080/api/Hotels", hotel);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteHotel(int id)
        {
            HttpResponseMessage response = await hotels.DeleteAsync("https://localhost:8080/api/Hotels" + id);
            return response.StatusCode;
        }
    }
}