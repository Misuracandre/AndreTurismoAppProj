using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.AddressesService.Data
{
    public class AndreTurismoAppAddressesServiceContext : DbContext
    {
        public AndreTurismoAppAddressesServiceContext (DbContextOptions<AndreTurismoAppAddressesServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;
    }
}
