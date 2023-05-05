﻿using AndreTurismoApp.ExternalsService;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }


        [HttpGet]
        public async Task<List<Address>> GetAddress()
        {
            return await _addressService.GetAddress();
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {

            var newAddress = await _addressService.PostAddress(address);
            return new ObjectResult(newAddress)
            {
                StatusCode = 201
            };
        }
    }
}
