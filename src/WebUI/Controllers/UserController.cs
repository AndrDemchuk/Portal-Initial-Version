using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    //[Authorize]
    public class UserController : ApiControllerBase
    {
        [Route("{id}/UploadImage")]
        [HttpPost]
        public async Task<IActionResult> Upload(int id, [FromForm]IFormFile file)
        {
            var profilePhotoLink = await Mediator.Send(new CreateUserProfilePhotoCommand()
            {
                FormFile = file,
                UserId = id
            });

            return Ok(profilePhotoLink);
        }
    }
}
