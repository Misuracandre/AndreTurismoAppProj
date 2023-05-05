using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using AndreTurismoApp.Services.Producers;

namespace AndreTurismoApp.AddressesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AndreTurismoAppAddressesServiceContext _context;
        private readonly PostOfficesService _postOfficesService;
        private readonly ProducerAddressesService _producerAddressesService;

        public AddressesController(AndreTurismoAppAddressesServiceContext context, PostOfficesService postOfficesService, ProducerAddressesService producerAddressesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
            _producerAddressesService = producerAddressesService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            return await _context.Address.Include(a => a.IdCity).ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            var address = await _context.Address.Include(a => a.IdCity).Where(a => a.Id == id).FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return address;
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address, [FromServices] ProducerAddressesService producerAddressesService)
        {
          if (_context.Address == null)
          {
              return Problem("Entity set 'AndreTurismoAppAddressesServiceContext.Address'  is null.");
          }

            AddressDTO addreesDto = _postOfficesService.GetAddress(address.CEP).Result;
            var addressComplete = new Address(addreesDto);
            _producerAddressesService.PostMQAddresses(addressComplete);
            //_context.Address.Add(addressComplete);

            //await _context.SaveChangesAsync();

            return addressComplete;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
