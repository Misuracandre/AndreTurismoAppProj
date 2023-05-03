using AndreTurismoApp.AddressesService.Controllers;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.CustomersService.Controllers;
using AndreTurismoApp.CustomersService.Data;
using AndreTurismoApp.HotelsService.Controllers;
using AndreTurismoApp.HotelsService.Data;
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
    public class UnitTestHotel
    {
        private DbContextOptions<AndreTurismoAppHotelsServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppHotelsServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                context.Hotel.Add(new Hotel { Id = 1, Name = "Andre 1", Value = 50, IdAddress = new Address { Id = 1, Street = "Rua 1", CEP = "14801095", IdCity = new City() { Id = 1, Description = "City1" } } });
                context.Hotel.Add(new Hotel { Id = 2, Name = "Andre 2", Value = 50, IdAddress = new Address { Id = 2, Street = "Rua 2", CEP = "14801094", IdCity = new City() { Id = 2, Description = "City2" } } });
                context.Hotel.Add(new Hotel { Id = 3, Name = "Andre 3", Value = 50, IdAddress = new Address { Id = 3, Street = "Rua 3", CEP = "14801094", IdCity = new City() { Id = 3, Description = "City3" } } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                HotelsController clientController = new HotelsController(context, null);
                IEnumerable<Hotel> clients = clientController.GetHotel().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                int clientId = 2;
                HotelsController clientController = new HotelsController(context, null);
                Hotel client = clientController.GetHotelById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 4,
                Name = "Rua 10",
                Value = 50,
                IdAddress = new Address() { Id = 1, Street = "Rua 10", CEP = "14801095", IdCity = new City() { Id = 1, Description = "City1" } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                HotelsController clientController = new HotelsController(context, new PostOfficesService());
                Hotel ht = clientController.PostHotel(hotel).Result.Value;
                Assert.Equal("Rua 10", ht.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 3,
                Name = "Rua 10 Alterada",
                Value = 50,
                IdAddress = new Address() { Id = 1, Street = "Rua 10 alterada", CEP = "14801096", IdCity = new City() { Id = 1, Description = "City1 alterada" } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                HotelsController clientController = new HotelsController(context, null);
                Hotel ht = clientController.PutHotel(3, hotel).Result.Value;
                Assert.Equal("Rua 10 Alterada", ht.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelsServiceContext(options))
            {
                HotelsController hotelsController = new HotelsController(context, null);
                Hotel hotel = hotelsController.DeleteHotel(2).Result.Value;
                Assert.Null(hotel);
            }
        }
    }
}