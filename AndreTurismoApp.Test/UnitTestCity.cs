﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.AddressesService.Controllers;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.CitiesService.Controllers;
using AndreTurismoApp.CitiesService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.Test
{
    public class UnitTestCity
    {
        private DbContextOptions<AndreTurismoAppCitiesServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppCitiesServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                context.City.Add(new City { Id = 1, Description = "City1" });
                context.City.Add(new City { Id = 2, Description = "City2" });
                context.City.Add(new City { Id = 3, Description = "City3" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                CitiesController clientController = new CitiesController(context, null);
                IEnumerable<City> clients = clientController.GetCity().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                int clientId = 2;
                CitiesController clientController = new CitiesController(context, null);
                City client = clientController.GetCityById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            City city = new City()
            {
                Id = 4,
                Description = "Rua 10",
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                CitiesController clientController = new CitiesController(context, new PostOfficesService());
                City ct = clientController.PostCity(city).Result.Value;
                Assert.Equal("Avenida Alberto Benassi", ct.Description);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            City city = new City()
            {
                Id = 3,
                Description = "Rua 10 Alterada",
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                CitiesController clientController = new CitiesController(context, null);
                City ct = clientController.PutCity(3, city).Result.Value;
                Assert.Equal("Rua 10 Alterada", ct.Description);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCitiesServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                Address address = addressController.DeleteAddress(2).Result.Value;
                Assert.Null(address);
            }
        }
    }
}
