using BvAcademyPortal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface IProfilePhotoManager
    {
        Task<string> UploadAsync(ProfilePhoto profilePhoto, ProfilePhotoDetails details);
        Task<byte[]> GetAsync(string profilePhotoPath);
    }
}
