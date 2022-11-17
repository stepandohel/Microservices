using Basket.Application.Models.Order;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var orders = await _orderService.GetAllAsync(token);
            
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderPostModel orderPostModel, CancellationToken token)
        {
            var orderId = await _orderService.CreateAsync(orderPostModel, token);
            
            return Created(nameof(ProductController), orderId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, OrderPutModel orderPutModel, CancellationToken token)
        {
            var updatedOrder = await _orderService.UpdateAsync(id, orderPutModel, token);
            
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var camDeleteOrder = await _orderService.DeleteAsync(id, token);
            
            if (camDeleteOrder)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
