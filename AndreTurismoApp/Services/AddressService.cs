using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly HttpClient addresses = new HttpClient();
        public async Task<List<Address>> GetAddress()
        {
            try
            {
                HttpResponseMessage response = await addresses.GetAsync("https://localhost:7054/api/Addresses");
                response.EnsureSuccessStatusCode();
                string addressJson = await response.Content.ReadAsStringAsync();
                var addressesList = JsonConvert.DeserializeObject<List<Address>>(addressJson);
                return addressesList;
            }
            catch (HttpRequestException e)
            {
                return new List<Address>();
            }
        }

        public async Task<Address> GetAddressById(int id)
        {
            try
            {
                HttpResponseMessage response = await addresses.GetAsync("https://localhost:7054/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string addressJson = await response.Content.ReadAsStringAsync();
                var addressesList = JsonConvert.DeserializeObject<Address>(addressJson);
                return addressesList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostAddress(Address address)
        {
            HttpResponseMessage response = await addresses.PostAsJsonAsync("https://localhost:7054/api/Addresses/", address);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutAddress(Address address)
        {
            HttpResponseMessage response = await addresses.PutAsJsonAsync("https://localhost:7054/api/Addresses/", address);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteAddress(int id)
        {
            HttpResponseMessage response = await addresses.DeleteAsync("https://localhost:7054/api/Addresses/" + id);
            return response.StatusCode;
        }
    }
}
