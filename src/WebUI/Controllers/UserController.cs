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
        private StorageConfiguration StorageConfiguration { get; }
        private readonly IProfilePhotoManager _profilePhotoManager;

        public UserController(IConfiguration configuration, IProfilePhotoManager profilePhotoManager)
        {
            StorageConfiguration = configuration.GetSection("StorageConfiguration").Get<StorageConfiguration>();
            _profilePhotoManager = profilePhotoManager;
        }

        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            byte[] content;

            using (MemoryStream ms = new())
            {
                await file.CopyToAsync(ms);
                content = ms.ToArray();
            }

            var profilePhotoLink = await Mediator.Send(new CreateUserProfilePhotoCommand()
            {
                ProfilePhoto = new ProfilePhoto() { FileContent = content, FIleExtenstion = Path.GetExtension(file.FileName) },
                StorageConfiguration = StorageConfiguration
            });

            return Ok(profilePhotoLink);
        }
    }
}
