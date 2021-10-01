using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Security;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Application.Courses.Commands.PurgeCourses;
using BvAcademyPortal.Application.Courses.Queries.ExportTodos;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Courses.Commands
{
    using static Testing;

    public class PurgeCoursesTests : TestBase
    {
        [Test]
        public void ShouldDenyAnonymousUser()
        {
            var command = new PurgeCoursesCommand();

            command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }

        [Test]
        public async Task ShouldDenyNonAdministrator()
        {
            await RunAsDefaultUserAsync();

            var command = new PurgeCoursesCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldAllowAdministrator()
        {
            await RunAsAdministratorAsync();

            var command = new PurgeCoursesCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should().NotThrow<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldDeleteAllLists()
        {
            await RunAsAdministratorAsync();

            await SendAsync(new CreateCourseCommand
            {
                Title = "New List #1"
            });

            await SendAsync(new CreateCourseCommand
            {
                Title = "New List #2"
            });

            await SendAsync(new CreateCourseCommand
            {
                Title = "New List #3"
            });

            await SendAsync(new PurgeCoursesCommand());

            var count = await CountAsync<Course>();

            count.Should().Be(0);
        }
    }
}
