using Azure.Storage.Blobs;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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

        public async Task<string> UploadAsync(IFormFile file, ProfilePhotoDetails details)
        {
            var fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

            var blobContainer = _blobServiceClient.GetBlobContainerClient(details.BlobName);
            var blobClient = blobContainer.GetBlobClient(fileName);

            var filePath = string.Concat(blobClient.Uri.ToString(), "/", fileName);

            await blobClient.UploadAsync(file.OpenReadStream());

            return filePath;
        }
    }
}
