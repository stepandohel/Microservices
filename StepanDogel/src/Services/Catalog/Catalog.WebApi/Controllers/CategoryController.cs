using Catalog.Application.Category.Commands.Create;
using Catalog.Application.Category.Commands.Delete;
using Catalog.Application.Category.Commands.Update;
using Catalog.Application.Category.Queries;
using Catalog.Application.Middleware.ServiceExceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromRoute] GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(request, cancellationToken);
            
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createdCategoryId = await _mediator.Send(request, cancellationToken);
            
            return Created(nameof(CategoryController), createdCategoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!id.Equals(request.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }

            var updatedCategory = await _mediator.Send(request, cancellationToken);
           
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, [FromBody] DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!id.Equals(request.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }

            var canDeleteCategory = await _mediator.Send(request, cancellationToken);
           
            if (canDeleteCategory)
                return NoContent();
            
            return BadRequest();
        }
    }
}
