using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Services
{
    public class LocalProfilePhotoUploadService : IProfilePhotoManager
    {
        public Task<byte[]> GetAsync(string profilePhotoPath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(ProfilePhoto profilePhoto, ProfilePhotoDetails details)
        {
            if(!Directory.Exists(details.Path))
            {
                Directory.CreateDirectory(details.Path);
            }

            var fileName = Path.Combine(Directory.GetCurrentDirectory(), details.Path, string.Concat(Guid.NewGuid().ToString(), profilePhoto.FIleExtenstion));

            await File.WriteAllBytesAsync(fileName, profilePhoto.FileContent);

            return fileName;
        }
    }
}
