using Basket.Application.Models.Product;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var products = await _productService.GetAllAsync(token);
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductPostModel productPostModel, CancellationToken token)
        {
            var productId = await _productService.CreateAsync(productPostModel, token);
            
            return Created(nameof(ProductController), productId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ProductPutModel productPutModel, CancellationToken token)
        {
            var updatedProduct = await _productService.UpdateAsync(id, productPutModel, token);
            
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var canDeleteOrder = await _productService.DeleteAsync(id, token);
            
            if (canDeleteOrder)
                return NoContent();

            return BadRequest();
        }
    }
}
