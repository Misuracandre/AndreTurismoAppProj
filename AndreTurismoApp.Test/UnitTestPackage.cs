using AndreTurismoApp.AddressesService.Controllers;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.CustomersService.Controllers;
using AndreTurismoApp.CustomersService.Data;
using AndreTurismoApp.PackagesService.Controllers;
using AndreTurismoApp.PackagesService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using AndreTurismoApp.PackagesService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;

namespace AndreTurismoApp.Test
{
    public class UnitTestPackage
    {
        private DbContextOptions<AndreTurismoAppPackagesServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppPackagesServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                context.Package.Add(new Package
                {
                    IdHotel = new Hotel
                    {
                        Name = "Hotel",
                        Value = 50,
                        IdAddress = new()
                        {
                            Street = "Rua 10 alterada",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City1 alterada" }
                        }
                    },
                    IdTicket = new Ticket()
                    {
                        Value = 50,
                        IdOrigin = new Address() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } },
                        IdDestination = new Address() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada2" } }

                    },
                    IdCustomer = new Customer()
                    {
                        Name = "Andre",
                        Phone = "12341213",
                        IdAddress = new Address()
                        {
                            Street = "Rua 12",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City3" }
                        }
                    }
                });
                context.Package.Add(new Package
                {
                    IdHotel = new Hotel
                    {
                        Name = "Hotel",
                        Value = 50,
                        IdAddress = new()
                        {
                            Street = "Rua 10 alterada",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City1 alterada" }
                        }
                    },
                    IdTicket = new Ticket()
                    {
                        Value = 50,
                        IdOrigin = new Address() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } },
                        IdDestination = new Address() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada2" } }

                    },
                    IdCustomer = new Customer()
                    {
                        Name = "Andre",
                        Phone = "12341213",
                        IdAddress = new Address()
                        {
                            Street = "Rua 12",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City3" }
                        }
                    }
                });
                context.Package.Add(new Package
                {
                    IdHotel = new Hotel
                    {
                        Name = "Hotel",
                        Value = 50,
                        IdAddress = new()
                        {
                            Street = "Rua 10 alterada",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City1 alterada" }
                        }
                    },
                    IdTicket = new Ticket()
                    {
                        Value = 50,
                        IdOrigin = new Address() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } },
                        IdDestination = new Address() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada2" } }

                    },
                    IdCustomer = new Customer()
                    {
                        Name = "Andre",
                        Phone = "12341213",
                        IdAddress = new Address()
                        {
                            Street = "Rua 12",
                            CEP = "14801094",
                            IdCity = new City() { Description = "City3" }
                        }
                    }
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                PackagesController clientController = new PackagesController(context, null);
                IEnumerable<Package> clients = clientController.GetPackage().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                int clientId = 2;
                PackagesController clientController = new PackagesController(context, null);
                Package client = clientController.GetPackageById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Package package = new Package()
            {
                IdHotel = new Hotel() { Name = "Hotel", Value = 50, IdAddress = new() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } } },
                IdTicket = new Ticket() { Value = 50, IdOrigin = new Address() { Street = "Rua 10 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada" } }, IdDestination = new Address() { Street = "Rua 11 alterada", CEP = "14801094", IdCity = new City() { Description = "City1 alterada2" } } },
                Value = 50,
                IdCustomer = new Customer() { Name = "Andre", Phone = "12341213", IdAddress = new Address() { Street = "Rua 12", CEP = "14801094", IdCity = new City() { Description = "City3" } } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                PackagesController clientController = new PackagesController(context, new PostOfficesService());
                Package pc = clientController.PostPackage(package).Result.Value;
                Assert.Equal(50, pc.Value);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Package package = new Package()
            {
                Id = 3,
                Value = 50
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                PackagesController clientController = new PackagesController(context, null);
                Package pc = clientController.PutPackage(3, package).Result.Value;
                Assert.Equal(50, pc.Value);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppPackagesServiceContext(options))
            {
                PackagesController PackagesController = new PackagesController(context, null);
                Package package = PackagesController.DeletePackage(2).Result.Value;
                Assert.Null(package);
            }
        }
    }
}