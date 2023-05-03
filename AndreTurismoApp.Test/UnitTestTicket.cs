using AndreTurismoApp.AddressesService.Controllers;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.CustomersService.Controllers;
using AndreTurismoApp.CustomersService.Data;
using AndreTurismoApp.TicketsService.Controllers;
using AndreTurismoApp.TicketsService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using AndreTurismoApp.TicketsService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AndreTurismoApp.Test
{
    public class UnitTestTicket
    {
        private DbContextOptions<AndreTurismoAppTicketsServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppTicketsServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                context.Ticket.Add(new Ticket { IdOrigin = new Address() { Street = "Rua 1", CEP = "14801094", IdCity = new City() { Description = "City1" } }, Value = 50, IdDestination = new Address { Street = "Rua 2", CEP = "14801094", IdCity = new City() { Description = "City2" } }, IdCustomer = new Customer() { Name = "Andre1", Phone = "12344124", IdAddress =  new() { Street = "Rua aquela ali1", CEP = "14801094", IdCity = new() { Description = "Araraquara1" } } } });
                context.Ticket.Add(new Ticket { IdOrigin = new Address() { Street = "Rua 2", CEP = "14801094", IdCity = new City() { Description = "City2" } }, Value = 50, IdDestination = new Address { Street = "Rua 3", CEP = "14801094", IdCity = new City() { Description = "City3" } }, IdCustomer = new Customer() { Name = "Andre2", Phone = "12344125", IdAddress =  new() { Street = "Rua aquela ali2", CEP = "14801094", IdCity = new() { Description = "Araraquara2" } } } });
                context.Ticket.Add(new Ticket { IdOrigin = new Address() { Street = "Rua 3", CEP = "14801094", IdCity = new City() { Description = "City3" } }, Value = 50, IdDestination = new Address { Street = "Rua 4", CEP = "14801094", IdCity = new City() { Description = "City4" } }, IdCustomer = new Customer() { Name = "Andre3", Phone = "12344126", IdAddress =  new() { Street = "Rua aquela ali3", CEP = "14801094", IdCity = new() { Description = "Araraquara3" } } } });
                
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                TicketsController clientController = new TicketsController(context, null);
                IEnumerable<Ticket> clients = clientController.GetTicket().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                int clientId = 2;
                TicketsController clientController = new TicketsController(context, null);
                Ticket client = clientController.GetTicketById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Ticket ticket = new Ticket()
            {
                IdOrigin = new Address() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } },
                IdDestination = new Address() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada2" } },
                Value = 50,
                IdCustomer = new Customer() { Name = "Andre", Phone = "12341213", IdAddress = new Address() { Street = "Rua 12", CEP = "14801094", IdCity = new City() { Description = "City3" } } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                TicketsController clientController = new TicketsController(context, new PostOfficesService());
                Ticket tc = clientController.PostTicket(ticket).Result.Value;
                Assert.Equal(50, tc.Value);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Ticket ticket = new Ticket()
            {
                Id = 3,
                IdOrigin = new() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new() { Description = "City1 alterada" } },
                IdDestination = new() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new() { Description = "City1 alterada2" } },
                Value = 50,
                IdCustomer = new() { Name = "Andre", Phone = "12341213", IdAddress = new() { Street = "Rua 12", CEP = "14801094", IdCity = new() { Description = "City3" } } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                TicketsController clientController = new TicketsController(context, null);
                Ticket tc = clientController.PutTicket(3, ticket).Result.Value;
                Assert.Equal(50, tc.Value);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppTicketsServiceContext(options))
            {
                TicketsController TicketsController = new TicketsController(context, null);
                Ticket ticket = TicketsController.DeleteTicket(2).Result.Value;
                Assert.Null(ticket);
            }
        }
    }
}