using Catalog.Application.Middleware.ServiceExceptions;
using Catalog.Mongo.Models;
using Catalog.Mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductMongoController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductMongoController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product, CancellationToken cancellationToken)
        {
            await _productService.CreateAsync(product);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] Product product, CancellationToken cancellationToken)
        {
            if (!id.Equals(product.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }

            await _productService.UpdateAsync(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
