using Basket.Application.Models.Product;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTestController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductTestController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var items = await _productService.GetAllAsync(token);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductPostModel productPostModel, CancellationToken token)
        {
            var item = await _productService.AddAsync(productPostModel, token);

            return Created(nameof(ProductTestController), item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, ProductPutModel productPutModel, CancellationToken token)
        {
            await _productService.UpdateAsync(id, productPutModel, token);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var isTrue = await _productService.DeleteAsync(id, token);
            if (isTrue)
                return NoContent();

            return BadRequest();
        }
    }
}
