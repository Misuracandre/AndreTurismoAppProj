using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.HotelsService.Data
{
    public class AndreTurismoAppHotelsServiceContext : DbContext
    {
        public AndreTurismoAppHotelsServiceContext (DbContextOptions<AndreTurismoAppHotelsServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
    }
}
