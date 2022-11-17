using Basket.Application.Models.Customer;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerTestController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerTestController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var items = await _customerService.GetAllAsync(token);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CustomerPostModel customerPostModel, CancellationToken token)
        {
            var item = await _customerService.AddAsync(customerPostModel, token);

            return Created(nameof(CustomerTestController), item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, CustomerPutModel customerPutModel, CancellationToken token)
        {
            await _customerService.UpdateAsync(id, customerPutModel, token);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var isTrue = await _customerService.DeleteAsync(id, token);
            if (isTrue)
                return NoContent();

            return BadRequest();
        }
    }
}
