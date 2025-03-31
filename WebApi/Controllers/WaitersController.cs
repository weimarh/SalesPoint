using Application.UseCases.Users.Create;
using Application.UseCases.Users.GetAll;
using Application.UseCases.Users.Update;
using Application.UseCases.Waiters.Delete;
using Application.UseCases.Waiters.GetById;
using Domain.DomainErrors;
using Domain.Entities.Waiters;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("waiters")]
    public class WaitersController : ApiController
    {
        private readonly ISender _mediator;

        public WaitersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> GetAll()
        {
            var waiters = await _mediator.Send(new GetAllUsersQuery());

            return waiters.Match(
                waiters => Ok(waiters),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var waiter = await _mediator.Send(new GetWaiterByIdQuery(id));

            return waiter.Match(
                waiters => Ok(waiters),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                waiters => Ok(waiters),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    WaiterErrors.WaiterNotFound,
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                waiters => Ok(waiters),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteWaiterCommand(id));

            return result.Match(
                waiters => Ok(waiters),
                errors => Problem(errors)
            );
        }
    }
}
