using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class CustomerService
    {
        static readonly HttpClient customerClient = new HttpClient();
        public async Task<List<Customer>> GetCustomer()
        {
            try
            {
                HttpResponseMessage response = await CustomerService.customerClient.GetAsync("https://localhost:7141/api/Addresses");
                response.EnsureSuccessStatusCode();
                string customerJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Customer>>(customerJson);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
