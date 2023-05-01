using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.CustomersService.Data
{
    public class AndreTurismoAppCustomersServiceContext : DbContext
    {
        public AndreTurismoAppCustomersServiceContext (DbContextOptions<AndreTurismoAppCustomersServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Customer> Customer { get; set; } = default!;
    }
}
