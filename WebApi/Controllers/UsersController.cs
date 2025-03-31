using Application.UseCases.Users.Create;
using Application.UseCases.Users.Delete;
using Application.UseCases.Users.GetAll;
using Application.UseCases.Users.GetById;
using Application.UseCases.Users.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("users")]
    public class UsersController : ApiController
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return users.Match(
                users => Ok(users),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            return user.Match(
                user => Ok(user),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                user => Ok(user),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    UserErrors.UserNotFound,
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                user => Ok(user),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return result.Match(
                user => Ok(user),
                errors => Problem(errors)
            );
        }
    }
}
