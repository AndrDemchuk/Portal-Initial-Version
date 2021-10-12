using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

            RuleFor(c => c.FormFile).NotNull().DependentRules(() =>
            {
                RuleFor(c => c.FormFile.FileName)
                    .NotNull()
                    .Must(fn => _allowedExtensions.Contains(Path.GetExtension(fn)))
                    .WithMessage("Invalid file extension.");
                RuleFor(c => c.FormFile.Length)
                    .InclusiveBetween(_minSize, _maxSize)
                    .WithMessage("Ivalid file size.");
            });
        }
    }
}
