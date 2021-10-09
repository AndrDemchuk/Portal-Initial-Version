using FluentValidation;
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
        static readonly string[] _allowedExtensions = new string[]
        {
            ".jpeg",
            ".png"
        };

        static readonly int _maxSize = 30 * 1_048_576;
        static readonly int _minSize = 1;

        public CreateUserProfilePhotoCommandValidator()
        {
            RuleFor(c => c.FormFile).NotNull();
            RuleFor(c => c.FormFile.FileName).Must(n => _allowedExtensions.Contains(Path.GetExtension(n)));
            RuleFor(c => c.FormFile.Length).InclusiveBetween(_minSize, _maxSize);
            RuleFor(c => c.Details.BlobName).NotEmpty();
        }
    }
}
