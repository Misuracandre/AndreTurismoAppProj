using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.TicketsService.Data;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.TicketsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AndreTurismoAppTicketsServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public TicketsController(AndreTurismoAppTicketsServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            return await _context.Ticket
                .Include(t => t.IdOrigin)
                    .ThenInclude(a => a.IdCity)
                .Include(t => t.IdDestination)
                    .ThenInclude(a => a.IdCity)
                .Include(t => t.IdCustomer)
                    .ThenInclude(c => c.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket
                .Include(t => t.IdOrigin)
                    .ThenInclude(a => a.IdCity)
                .Include(t => t.IdDestination)
                    .ThenInclude(a => a.IdCity)
                .Include(t => t.IdCustomer)
                    .ThenInclude(c => c.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'AndreTurismoAppTicketsServiceContext.Ticket'  is null.");
            }

            AddressDTO addressDtoOrigin = _postOfficesService.GetAddress(ticket.IdOrigin.CEP).Result;
            var completeAddress = new Address(addressDtoOrigin);
            //completeAddress.IdCity = new City() { Description = addressDtoOrigin.City };
            ticket.IdOrigin = completeAddress;

            AddressDTO addressDtoDestination = _postOfficesService.GetAddress(ticket.IdDestination.CEP).Result;
            var addressComplete = new Address(addressDtoDestination);
            //addressComplete.IdCity = new City() { Description = addressDtoDestination.City };
            ticket.IdDestination = addressComplete;

            AddressDTO addressDtoCustomer = _postOfficesService.GetAddress(ticket.IdCustomer.IdAddress.CEP).Result;
            var addressCompleteCustomer = new Address(addressDtoCustomer);
            //addressCompleteCustomer.IdCity = new City() { Description = addressDtoCustomer.City };
            ticket.IdCustomer.IdAddress = addressCompleteCustomer;

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
