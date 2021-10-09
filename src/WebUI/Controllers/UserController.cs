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
        private readonly ProfilePhotoDetails _profilePhotoDetails;

        public UserController(IConfiguration configuration)
        {
            _profilePhotoDetails = new ProfilePhotoDetails() 
            { 
                BlobName = configuration.GetSection("StorageConfiguration").GetSection("BlobName").Value 
            };
        }

        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            var profilePhotoLink = await Mediator.Send(new CreateUserProfilePhotoCommand()
            {
                FormFile = file,
                Details = _profilePhotoDetails
            });

            return Ok(profilePhotoLink);
        }
    }
}
