using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Services
{
    public class LocalProfilePhotoUploadService : IProfilePhotoManager
    {
        public async Task<string> UploadAsync(IFormFile profilePhoto, ProfilePhotoDetails details)
        {
            if(!Directory.Exists(details.BlobName))
            {
                Directory.CreateDirectory(details.BlobName);
            }

            var fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(profilePhoto.FileName));

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), details.BlobName, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await profilePhoto.CopyToAsync(fs);
            }

            return filePath;
        }
    }
}
