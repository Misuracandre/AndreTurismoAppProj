using AndreTurismoApp.AddressesService.Controllers;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.CustomersService.Controllers;
using AndreTurismoApp.CustomersService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
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
    public class UnitTestCustomer
    {
        private DbContextOptions<AndreTurismoAppCustomersServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppCustomersServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                context.Customer.Add(new Customer { Id = 1, Name = "Andre 1", Phone = "987654321", IdAddress = new Address { Id = 1, Street = "Rua 1", CEP = "14801095", IdCity = new City() { Id = 1, Description = "City1" } } });
                context.Customer.Add(new Customer { Id = 2, Name = "Andre 2", Phone = "987654322", IdAddress = new Address { Id = 2, Street = "Rua 2", CEP = "14801094", IdCity = new City() { Id = 2, Description = "City2" } } });
                context.Customer.Add(new Customer { Id = 3, Name = "Andre 3", Phone = "987654323", IdAddress = new Address { Id = 3, Street = "Rua 3", CEP = "14801094", IdCity = new City() { Id = 3, Description = "City3" } } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                CustomersController clientController = new CustomersController(context, null);
                IEnumerable<Customer> clients = clientController.GetCustomer().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                int clientId = 2;
                CustomersController clientController = new CustomersController(context, null);
                Customer client = clientController.GetCustomerById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Customer customer = new Customer()
            {
                Id = 4,
                Name = "Rua 10",
                Phone = "14804300",
                IdAddress = new Address() { Id = 1, Street = "Rua 10", CEP = "14801095", IdCity = new City() { Id = 1, Description = "City1" } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                CustomersController clientController = new CustomersController(context, new PostOfficesService());
                Customer ct = clientController.PostCustomer(customer).Result.Value;
                Assert.Equal("Rua 10", ct.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Customer customer = new Customer()
            {
                Id = 3,
                Name = "Rua 10 Alterada",
                Phone = "14804301",
                IdAddress = new Address() { Id = 1, Street = "Rua 10 alterada", CEP = "14801096", IdCity = new City() { Id = 1, Description = "City1 alterada" } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                CustomersController clientController = new CustomersController(context, null);
                Customer ct = clientController.PutCustomer(3, customer).Result.Value;
                Assert.Equal("Rua 10 Alterada", ct.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCustomersServiceContext(options))
            {
                CustomersController customersController = new CustomersController(context, null);
                Customer customer = customersController.DeleteCustomer(2).Result.Value;
                Assert.Null(customer);
            }
        }
    }
}