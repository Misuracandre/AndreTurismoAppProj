using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.TicketsService.Data
{
    public class AndreTurismoAppTicketsServiceContext : DbContext
    {
        public AndreTurismoAppTicketsServiceContext (DbContextOptions<AndreTurismoAppTicketsServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
    }
}
