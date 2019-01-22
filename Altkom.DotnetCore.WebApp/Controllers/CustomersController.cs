using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerService.Get();

            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = customerService.Get(id);

            return Ok(customer);
        }


        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2
        [HttpGet("{lastname}")]
        public IActionResult Get(string lastname)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetActive()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            customerService.Add(customer);

            return CreatedAtRoute(new { id = customer.Id }, customer);
        }

        
    }
}
