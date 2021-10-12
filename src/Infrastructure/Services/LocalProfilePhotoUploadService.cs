using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Services
{
    public class LocalProfilePhotoUploadService : IProfilePhotoManager
    {
        public LocalProfilePhotoUploadService(IOptions<StorageConfiguration> options)
        {
            Configuration = options.Value;
        }

        public StorageConfiguration Configuration { get; }

        public async Task<string> UploadAsync(IFormFile profilePhoto)
        {
            if(!Directory.Exists(Configuration.BlobName))
            {
                Directory.CreateDirectory(Configuration.BlobName);
            }

            var fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(profilePhoto.FileName));

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), Configuration.BlobName, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await profilePhoto.CopyToAsync(fs);
            }

            return filePath;
        }
    }
}
