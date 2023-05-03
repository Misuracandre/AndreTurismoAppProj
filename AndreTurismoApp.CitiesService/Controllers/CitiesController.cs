﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.CitiesService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using System.Net;

namespace AndreTurismoApp.CitiesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AndreTurismoAppCitiesServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public CitiesController(AndreTurismoAppCitiesServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCity()
        {
          if (_context.City == null)
          {
              return NotFound();
          }
            return await _context.City.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
          if (_context.City == null)
          {
              return NotFound();
          }
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<City>> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
          if (_context.City == null)
          {
              return Problem("Entity set 'AndreTurismoAppCitiesServiceContext.City'  is null.");
          }

            AddressDTO addreesDto = _postOfficesService.GetAddress(city.Description).Result;
            var addressComplete = new Address(addreesDto);
            var newCity = new City { Description = addressComplete.ToString() };
            _context.City.Add(newCity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = newCity.Id }, newCity);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
