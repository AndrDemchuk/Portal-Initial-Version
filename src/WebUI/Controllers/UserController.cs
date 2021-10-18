using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Application.Users.Commands.CreateUser;
using BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto;
using BvAcademyPortal.Application.Users.Commands.DeleteUser;
using BvAcademyPortal.Application.Users.Commands.UpdateUser;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;


        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
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









        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            var profilePhotoLink = await Mediator.Send(new CreateUserProfilePhotoCommand()
            {
                FormFile = file,
                UserId = _currentUserService.UserId
            });

            return Ok(profilePhotoLink);
        }
    }
}
