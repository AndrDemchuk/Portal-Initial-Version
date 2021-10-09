using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto
{
    public class CreateUserProfilePhotoCommand : IRequest<string>
    {
        public IFormFile FormFile { get; set; }
        public ProfilePhotoDetails Details { get; set; }
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
            var profilePhotoLink =  await _profilePhotoManager.UploadAsync(request.FormFile, request.Details);

            return profilePhotoLink;
        }
    }
}
