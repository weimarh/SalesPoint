using Application.UseCases.ShoppingCarts.Create;
using Application.UseCases.ShoppingCarts.Delete;
using Application.UseCases.ShoppingCarts.GetAll;
using Application.UseCases.ShoppingCarts.GetById;
using Application.UseCases.ShoppingCarts.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("shoppingCarts")]
    public class ShoppingCartsController : ApiController
    {
        private readonly ISender _mediator;

        public ShoppingCartsController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shoppingCarts = await _mediator.Send(new GetAllShoppingCartsQuery());

            return shoppingCarts.Match(
                shoppingCarts => Ok(shoppingCarts),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var shoppingCart = await _mediator.Send(new GetShoppingCartByIdQuery(id));

            return shoppingCart.Match(
               shoppingCart => Ok(shoppingCart),
               errors => Problem(errors)
           );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShoppingCartCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                shoppingCart => Ok(shoppingCart),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateShoppingCartCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    ShoppingCartErrors.ShoppingCartNotFound
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                shoppingCart => Ok(shoppingCart),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteShoppingCartCommand(id));

            return result.Match(
                shoppingCart => Ok(shoppingCart),
                errors => Problem(errors)
            );
        }
    }
}
