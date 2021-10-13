using BvAcademyPortal.Application.Common.Security;
using BvAcademyPortal.Application.Users.Commands.CreateUser;
using BvAcademyPortal.Application.Users.Commands.DeleteUser;
using BvAcademyPortal.Application.Users.Commands.UpdateUser;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using BvAcademyPortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    //[Authorize]
    public class UserController:ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<UsersVm>> Get()
        {
            return await Mediator.Send(new GetUsersQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }
    }
}
