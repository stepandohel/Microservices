using Basket.Application.Models.Customer;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var customers = await _customerService.GetAllAsync(token);
            
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerCreateModel customerPostModel, CancellationToken token)
        {
            var customerId = await _customerService.CreateAsync(customerPostModel, token);
            
            return Created(nameof(CustomerController), customerId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CustomerUpdateModel customerPutModel, CancellationToken token)
        {
            var updatedCustomer = await _customerService.UpdateAsync(id, customerPutModel, token);
            
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var canDeleteCustomer = await _customerService.DeleteAsync(id, token);
            
            if (canDeleteCustomer)
                return NoContent();

            return BadRequest();
        }
    }
}
