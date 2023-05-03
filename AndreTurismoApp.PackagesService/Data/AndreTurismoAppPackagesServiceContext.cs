using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.PackagesService.Data
{
    public class AndreTurismoAppPackagesServiceContext : DbContext
    {
        public AndreTurismoAppPackagesServiceContext (DbContextOptions<AndreTurismoAppPackagesServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Package> Package { get; set; } = default!;
    }
}
