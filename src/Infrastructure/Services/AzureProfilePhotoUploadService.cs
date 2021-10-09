using Azure.Storage.Blobs;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Services
{
    public class AzureProfilePhotoUploadService : IProfilePhotoManager
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureProfilePhotoUploadService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task<byte[]> GetAsync(string profilePhotoPath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(ProfilePhoto profilePhoto, ProfilePhotoDetails details)
        {
            var fileName = string.Concat(Guid.NewGuid().ToString(), profilePhoto.FIleExtenstion);

            var blobContainer = _blobServiceClient.GetBlobContainerClient(details.Path);
            var blobClient = blobContainer.GetBlobClient(fileName);

            using (MemoryStream ms = new(profilePhoto.FileContent, writable: false))
                await blobClient.UploadAsync(ms);

            return fileName;
        }
    }
}
