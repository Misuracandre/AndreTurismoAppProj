using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.HotelsService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.HotelsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AndreTurismoAppHotelsServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public HotelsController(AndreTurismoAppHotelsServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            return await _context.Hotel
                .Include(h => h.IdAddress)
                    .ThenInclude(a => a.IdCity)
                .ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelById(int id)
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotel
                .Include(h => h.IdAddress)
                    .ThenInclude(a => a.IdCity)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return hotel;
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          if (_context.Hotel == null)
          {
              return Problem("Entity set 'AndreTurismoAppHotelsServiceContext.Hotel'  is null.");
          }

            AddressDTO addressDto = _postOfficesService.GetAddress(hotel.IdAddress.CEP).Result;
            var completeAddress = new Address(addressDto);
            hotel.IdAddress = completeAddress;

            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
