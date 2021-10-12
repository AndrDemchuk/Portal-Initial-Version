using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Domain.Entities;
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
        public int UserId { get; set; }
    }

    public class CreateUserProfilePhotoCommandHandler : IRequestHandler<CreateUserProfilePhotoCommand, string>
    {
        private readonly IProfilePhotoManager _profilePhotoManager;
        private readonly IApplicationDbContext _context;

        public CreateUserProfilePhotoCommandHandler(IProfilePhotoManager profilePhotoManager, IApplicationDbContext context)
        {
            _profilePhotoManager = profilePhotoManager;
            _context = context;
        }

        public async Task<string> Handle(CreateUserProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var profilePhotoLink = await _profilePhotoManager.UploadAsync(request.FormFile);

            user.ProfilePhotoLink = profilePhotoLink;

            await _context.SaveChangesAsync(cancellationToken);

            return profilePhotoLink;
        }
    }
}
