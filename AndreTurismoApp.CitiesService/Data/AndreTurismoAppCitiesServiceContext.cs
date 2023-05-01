using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.CitiesService.Data
{
    public class AndreTurismoAppCitiesServiceContext : DbContext
    {
        public AndreTurismoAppCitiesServiceContext (DbContextOptions<AndreTurismoAppCitiesServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
