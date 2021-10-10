using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto
{
    public class CreateUserProfilePhotoCommandValidator: AbstractValidator<CreateUserProfilePhotoCommand>
    {
        public CreateUserProfilePhotoCommandValidator()
        {
            RuleFor(c => c.FormFile).NotNull();
            RuleFor(c => c.FormFile).SetValidator(new UserFileValidator());
            RuleFor(c => c.Details.BlobName).NotEmpty();
        }
    }

    public class UserFileValidator : AbstractValidator<IFormFile>
    {
        static readonly string[] _allowedExtensions = new string[]
        {
            ".jpeg",
            ".png"
        };

        static readonly int _maxSize = 30 * 1_048_576;
        static readonly int _minSize = 1;

        public UserFileValidator()
        {
            RuleFor(f => f.FileName).Must(fn => _allowedExtensions.Contains(Path.GetExtension(fn)));
            RuleFor(f => f.Length).InclusiveBetween(_minSize, _maxSize);
        }
    }
}
