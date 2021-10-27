using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Application.Courses.Commands.DeleteCourse;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Courses.Commands
{
    using static Testing;

    public class DeleteCourseTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCourseId()
        {
            var command = new DeleteCourseCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteCourse()
        {
            var listId = await SendAsync(new CreateCourseCommand
            {
                Title = "New List"
            });

            await SendAsync(new DeleteCourseCommand 
            { 
                Id = listId 
            });

            var list = await FindAsync<Course>(listId);

            list.Should().BeNull();
        }
    }
}
