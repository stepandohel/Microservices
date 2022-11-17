using Catalog.Application.Category.Commands.Create;
using Catalog.Application.Category.Commands.Delete;
using Catalog.Application.Category.Commands.Update;
using Catalog.Application.Category.Queries;
using Catalog.Application.Models.Category;
using Catalog.Domain.Entities;
using Catalog.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryTestController: ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryTestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryCommand request, CancellationToken token)
        {
            var id = await _mediator.Send(request, token);

            return Created(nameof(CategoryTestController),id);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCategoryCommand request, CancellationToken token)
        {
            var isTrue = await _mediator.Send(request, token);

            return Ok(isTrue);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCategoryCommand request, CancellationToken token)
        {
            var isTrue = await _mediator.Send(request, token);
            if(isTrue)
            return NoContent();
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromRoute] GetAllCategoryQuery request, CancellationToken token)
        {
            var items = await _mediator.Send(request, token);

            return Ok(items);
        }
    }
}