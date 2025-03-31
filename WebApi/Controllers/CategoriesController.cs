using Application.UseCases.Categories.Create;
using Application.UseCases.Categories.Delete;
using Application.UseCases.Categories.GetAll;
using Application.UseCases.Categories.GetById;
using Application.UseCases.Categories.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("categories")]
    public class CategoriesController : ApiController
    {
        private readonly ISender _mediator;

        public CategoriesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            return categories.Match(
                categories => Ok(categories),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));

            return category.Match(
                category => Ok(category),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                category => Ok(category),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    CategoryErrors.CategoryNotFound
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                category => Ok(category),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));

            return result.Match(
                category => Ok(category),
                errors => Problem(errors)
            );
        }
    }
}
