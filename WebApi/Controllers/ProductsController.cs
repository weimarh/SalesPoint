using Application.UseCases.Products.Create;
using Application.UseCases.Products.Delete;
using Application.UseCases.Products.GetAll;
using Application.UseCases.Products.GetById;
using Application.UseCases.Products.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("products")]
    public class ProductsController : ApiController
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            return products.Match(
                products => Ok(products),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            return product.Match(
                product => Ok(product),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                product => Ok(product),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    ProductErrors.ProductNotFound,
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                product => Ok(product),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return result.Match(
                product => Ok(product),
                errors => Problem(errors)
            );
        }
    }
}
