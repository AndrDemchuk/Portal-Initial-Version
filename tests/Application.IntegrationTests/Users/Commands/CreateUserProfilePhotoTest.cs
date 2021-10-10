using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Application.Users.Commands.CreateUserProfilePhoto;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Users.Commands
{
    public class CreateUserProfilePhotoTest
    {
        [TestCaseSource(nameof(ThrowValidationExceptionCases))]
        public void ShouldThrowValidationException(CreateUserProfilePhotoCommand command)
        {
            FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().Throw<ValidationException>();
        }

        static IEnumerable<CreateUserProfilePhotoCommand> ThrowValidationExceptionCases()
        {
            yield return new CreateUserProfilePhotoCommand();
            yield return new CreateUserProfilePhotoCommand()
            {
                FormFile = null,
                Details = new ProfilePhotoDetails() { BlobName = "testname"}
            };
            yield return new CreateUserProfilePhotoCommand() 
            { 
                FormFile = new TestUserProfilePhoto() { FileName = "testname.jpg", Length = 5 * 1_048_576 }, 
                Details = new ProfilePhotoDetails() { BlobName = "testname"}
            };
            yield return new CreateUserProfilePhotoCommand()
            {
                FormFile = new TestUserProfilePhoto() { FileName = "testname.jpeg", Length = 0 * 1_048_576 },
                Details = new ProfilePhotoDetails() { BlobName = "testname" }
            };
            yield return new CreateUserProfilePhotoCommand()
            {
                FormFile = new TestUserProfilePhoto() { FileName = "testname.jepg", Length = 5 * 1_048_576 },
                Details = new ProfilePhotoDetails() { BlobName = "" }
            };
        }

        class TestUserProfilePhoto : IFormFile
        {
            public string ContentType => throw new NotImplementedException();

            public string ContentDisposition => throw new NotImplementedException();

            public IHeaderDictionary Headers => throw new NotImplementedException();

            public long Length { get; set; }

            public string Name => throw new NotImplementedException();

            public string FileName { get; set; }

            public void CopyTo(Stream target)
            {
                throw new NotImplementedException();
            }

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Stream OpenReadStream()
            {
                throw new NotImplementedException();
            }
        }
    }
}
