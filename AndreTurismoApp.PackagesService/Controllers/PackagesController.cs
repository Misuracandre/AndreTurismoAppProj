using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.PackagesService.Data;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.PackagesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AndreTurismoAppPackagesServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public PackagesController(AndreTurismoAppPackagesServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            return await _context.Package
                .Include(p => p.IdCustomer)
                    .ThenInclude(c => c.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdHotel)
                    .ThenInclude(h => h.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdTicket)
                    .ThenInclude(t => t.IdOrigin)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdTicket)
                    .ThenInclude(t => t.IdDestination)
                        .ThenInclude(a => a.IdCity)
                .ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackageById(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package
                .Include(p => p.IdCustomer)
                    .ThenInclude(c => c.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdHotel)
                    .ThenInclude(h => h.IdAddress)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdTicket)
                    .ThenInclude(t => t.IdOrigin)
                        .ThenInclude(a => a.IdCity)
                .Include(p => p.IdTicket)
                    .ThenInclude(t => t.IdDestination)
                        .ThenInclude(a => a.IdCity)
                .FirstOrDefaultAsync(package => package.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return package;
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'AndreTurismoAppPackagesServiceContext.Package'  is null.");
            }

            AddressDTO addressDtoHotel = _postOfficesService.GetAddress(package.IdHotel.IdAddress.CEP).Result;
            var hotelAddress = new Address(addressDtoHotel);
            package.IdHotel.IdAddress = hotelAddress;

            AddressDTO addressDtoCustomer = _postOfficesService.GetAddress(package.IdCustomer.IdAddress.CEP).Result;
            var customerAddress = new Address(addressDtoCustomer);
            package.IdCustomer.IdAddress = customerAddress;

            AddressDTO addressDtoOrigin = _postOfficesService.GetAddress(package.IdTicket.IdOrigin.CEP).Result;
            var originAddress = new Address(addressDtoOrigin);
            package.IdTicket.IdOrigin = originAddress;

            AddressDTO addressDtoDestination = _postOfficesService.GetAddress(package.IdTicket.IdDestination.CEP).Result;
            var destinationAddress = new Address(addressDtoDestination);
            package.IdTicket.IdDestination = destinationAddress;

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return package;
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Package>> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}