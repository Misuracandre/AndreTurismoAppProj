using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Services
{
    public class CustomerService
    {
        static readonly HttpClient customers = new HttpClient();
        public async Task<List<Customer>> GetCustomer()
        {
            try
            {
                HttpResponseMessage response = await customers.GetAsync("https://localhost:7141/api/Clients");
                response.EnsureSuccessStatusCode();
                string customerJson = await response.Content.ReadAsStringAsync();
                var customerList = JsonConvert.DeserializeObject<List<Customer>>(customerJson);
                return customerList;
            }
            catch (HttpRequestException e)
            {
                return new List<Customer>();
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                HttpResponseMessage response = await customers.GetAsync("https://localhost:7141/api/Customers/" + id);
                response.EnsureSuccessStatusCode();
                string customerJson = await response.Content.ReadAsStringAsync();
                var customerList = JsonConvert.DeserializeObject<Customer>(customerJson);
                return customerList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostCustomer(Customer customer)
        {
            HttpResponseMessage response = await customers.PostAsJsonAsync("https://localhost:7141/api/Customers", customer);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutCustomer(Customer customer)
        {
            HttpResponseMessage response = await customers.PutAsJsonAsync("https://localhost:7141/api/Customers", customer);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCustomer(int id)
        {
            HttpResponseMessage response = await customers.DeleteAsync("https://localhost:7141/api/Customers/" + id);
            return response.StatusCode;
        }
    }
}
