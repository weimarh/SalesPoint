using Application.UseCases.Orders.Create;
using Application.UseCases.Orders.Delete;
using Application.UseCases.Orders.GetAll;
using Application.UseCases.Orders.GetById;
using Application.UseCases.Orders.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("orders")]
    public class OrdersController : ApiController
    {
        private readonly ISender _mediator;

        public OrdersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());

            return orders.Match(
                orders => Ok(orders),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            return order.Match(
                order => Ok(order),
                errors => Problem(errors)
            );
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                order => Ok(order),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand command)
        {
            if (command.OrderId == id)
            {
                List<Error> errors = new()
                {
                    OrderErrors.OrderNotFound
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                order => Ok(order),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));

            return result.Match(
                order => Ok(order),
                errors => Problem(errors)
            );
        }
    }
}
