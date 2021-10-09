using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto
{
    public class CreateUserProfilePhotoCommand: IRequest<string>
    {
        public ProfilePhoto ProfilePhoto { get; set; }
        public StorageConfiguration StorageConfiguration {  get; set; }

        public void Deconstruct(out ProfilePhoto profilePhoto, out StorageConfiguration configuration)
        {
            profilePhoto = ProfilePhoto;
            configuration = StorageConfiguration;
        }
    }

    public class CreateUserProfilePhotoCommandHandler : IRequestHandler<CreateUserProfilePhotoCommand, string>
    {
        private readonly IProfilePhotoManager _profilePhotoManager;

        public CreateUserProfilePhotoCommandHandler(IProfilePhotoManager profilePhotoManager)
        {
            _profilePhotoManager = profilePhotoManager;
        }

        public async Task<string> Handle(CreateUserProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var (profilePhoto, configuration) = request;

            if(!configuration.AllowedFileTypes.Contains(profilePhoto.FIleExtenstion))
            {
                throw new FileExtensionException($"Filetype: {profilePhoto.FIleExtenstion} is not allowed.");
            }

            if(profilePhoto.FileContent.Length > configuration.MaxSize || profilePhoto.FileContent.Length.Equals(0))
            {
                throw new FileSizeException("Invalid file size for file");
            }

            var details = new ProfilePhotoDetails() { Path = configuration.Path };

            var profilePhotoLink =  await _profilePhotoManager.UploadAsync(profilePhoto, details);

            return profilePhotoLink;
        }
    }
}
