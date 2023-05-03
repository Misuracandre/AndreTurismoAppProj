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
    public class ExternalTicketsService
    {
        static readonly HttpClient tickets = new HttpClient();
        public async Task<List<Ticket>> GetTicket()
        {
            try
            {
                HttpResponseMessage response = await tickets.GetAsync("https://localhost:8085/api/Tickets");
                response.EnsureSuccessStatusCode();
                string ticketsJson = await response.Content.ReadAsStringAsync();
                var ticketsList = JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson);
                return ticketsList;
            }
            catch (HttpRequestException e)
            {
                return new List<Ticket>();
            }
        }
        public async Task<Ticket> GetTicketById(int id)
        {
            try
            {
                HttpResponseMessage response = await tickets.GetAsync("https://localhost:8085/api/Tickets/" + id);
                response.EnsureSuccessStatusCode();
                string ticketsJson = await response.Content.ReadAsStringAsync();
                var ticketsList = JsonConvert.DeserializeObject<Ticket>(ticketsJson);
                return ticketsList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostTicket(Ticket ticket)
        {
            HttpResponseMessage response = await tickets.PostAsJsonAsync("https://localhost:8085/api/Tickets", ticket);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutTicket(Ticket ticket)
        {
            HttpResponseMessage response = await tickets.PutAsJsonAsync("https://localhost:8085/api/Tickets", ticket);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteTicket(int id)
        {
            HttpResponseMessage response = await tickets.DeleteAsync("https://localhost:8085/api/Tickets/" + id);
            return response.StatusCode;
        }
    }
}
