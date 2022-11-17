using Basket.Application.Models.Order;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderTestController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderTestController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var items = await _orderService.GetAllAsync(token);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(OrderPostModel orderPostModel, CancellationToken token)
        {
            var item = await _orderService.AddAsync(orderPostModel, token);

            return Created(nameof(ProductTestController), item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, OrderPutModel orderPutModel, CancellationToken token)
        {
            await _orderService.UpdateAsync(id, orderPutModel, token);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var isTrue = await _orderService.DeleteAsync(id, token);
            if (isTrue)
                return NoContent();

            return BadRequest();
        }
    }
}
