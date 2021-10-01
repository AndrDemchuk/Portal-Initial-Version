using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Courses.Commands
{
    using static Testing;

    public class CreateCourseTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCourseCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateCourseCommand
            {
                Title = "Shopping"
            });

            var command = new CreateCourseCommand
            {
                Title = "Shopping"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCourse()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateCourseCommand
            {
                Title = "Tasks"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<Course>(id);

            list.Should().NotBeNull();
            list.Title.Should().Be(command.Title);
            list.CreatedBy.Should().Be(userId);
            list.Created.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
