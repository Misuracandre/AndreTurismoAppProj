//using AndreTurismoApp.AddressesService.Controllers;
//using AndreTurismoApp.AddressesService.Data;
//using AndreTurismoApp.Models;
//using AndreTurismoApp.Services;
//using AndreTurismoApp.Services.Producers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using AndreTurismoApp.AddressesService;

//namespace AndreTurismoApp.Test
//{
//    public class UnitTestAddress
//    {
//        private DbContextOptions<AndreTurismoAppAddressesServiceContext> options;

//        private void InitializeDataBase()
//        {
//            // Create a Temporary Database
//            options = new DbContextOptionsBuilder<AndreTurismoAppAddressesServiceContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            // Insert data into the database using one instance of the context
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                context.Address.Add(new Address { Id = 1, Street = "Street 1", CEP = "123456789", IdCity = new City() { Id = 1, Description = "City1" } });
//                context.Address.Add(new Address { Id = 2, Street = "Street 2", CEP = "987654321", IdCity = new City() { Id = 2, Description = "City2" } });
//                context.Address.Add(new Address { Id = 3, Street = "Street 3", CEP = "159647841", IdCity = new City() { Id = 3, Description = "City3" } });
//                context.SaveChanges();
//            }
//        }

//        [Fact]
//        public void GetAll()
//        {
//            InitializeDataBase();

//            // Use a clean instance of the context to run the test
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                AddressesController clientController = new AddressesController(context, null, null);
//                IEnumerable<Address> clients = clientController.GetAddress().Result.Value;

//                Assert.Equal(3, clients.Count());
//            }
//        }

//        [Fact]
//        public void GetbyId()
//        {
//            InitializeDataBase();

//            // Use a clean instance of the context to run the test
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                int clientId = 2;
//                AddressesController clientController = new AddressesController(context, null, null);
//                Address client = clientController.GetAddressById(clientId).Result.Value;
//                Assert.Equal(2, client.Id);
//            }
//        }

//        [Fact]
//        public void Create()
//        {
//            InitializeDataBase();

//            Address address = new Address()
//            {
//                Id = 4,
//                Street = "Rua 10",
//                CEP = "14804300",
//                IdCity = new() { Id = 10, Description = "City 10" }
//            };

//            var producer = new ProducerAddressesService(new RabbitMQ
//            {
//                HostName = "localhost",
//                UserName = "guest",
//                Password = "guest",
//                QueueName = "ProducerAddressesService"
//            });

//            // Use a clean instance of the context to run the test
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                AddressesController clientController = new AddressesController(context, new PostOfficesService(), producer);
//                Address ad = clientController.PostAddress(address).Result.Value;
//                Assert.Equal("Avenida Alberto Benassi", ad.Street);
//            }
//        }

//        [Fact]
//        public void Update()
//        {
//            InitializeDataBase();

//            Address address = new Address()
//            {
//                Id = 3,
//                Street = "Rua 10 Alterada",
//                IdCity = new() { Id = 10, Description = "City 10 alterada" }
//            };

//            // Use a clean instance of the context to run the test
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                AddressesController clientController = new AddressesController(context, null, null);
//                Address ad = clientController.PutAddress(3, address).Result.Value;
//                Assert.Equal("Rua 10 Alterada", ad.Street);
//            }
//        }

//        [Fact]
//        public void Delete()
//        {
//            InitializeDataBase();

//            // Use a clean instance of the context to run the test
//            using (var context = new AndreTurismoAppAddressesServiceContext(options))
//            {
//                AddressesController addressController = new AddressesController(context, null, null);
//                Address address = addressController.DeleteAddress(2).Result.Value;
//                Assert.Null(address);
//            }
//        }
//    }
//}