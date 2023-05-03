using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class PostOfficesService
    {
        static readonly HttpClient address = new HttpClient();
        public async Task<AddressDTO> GetAddress(string cep)
        {
            try
            {
                HttpResponseMessage response = await PostOfficesService.address.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string postOfficeJson = await response.Content.ReadAsStringAsync();
                var postOfficeDto = JsonConvert.DeserializeObject<AddressDTO>(postOfficeJson);
                return postOfficeDto;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
