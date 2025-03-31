using Application.UseCases.Roles.Create;
using Application.UseCases.Roles.Delete;
using Application.UseCases.Roles.GetAll;
using Application.UseCases.Roles.GetById;
using Application.UseCases.Roles.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("roles")]
    public class RolesController : ApiController
    {
        private readonly ISender _mediator;

        public RolesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());

            return roles.Match(
                roles => Ok(roles),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));

            return role.Match(
                role => Ok(role),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                role => Ok(role),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    RoleErrors.RoleNotFound,
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                role => Ok(role),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));

            return result.Match(
                role => Ok(role),
                errors => Problem(errors)
            );
        }
    }
}
