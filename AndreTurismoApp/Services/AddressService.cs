using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly HttpClient addressClient = new HttpClient();
        public async Task<List<Address>> GetAddress()
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.GetAsync("https://localhost:7060/api/Addresses");
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Address>>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Address> FindAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await addressClient.GetAsync($"https://localhost:7060/api/Addresses/{id}");
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}