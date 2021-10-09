using BvAcademyPortal.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface IProfilePhotoManager
    {
        Task<string> UploadAsync(IFormFile file, ProfilePhotoDetails details);
    }
}
