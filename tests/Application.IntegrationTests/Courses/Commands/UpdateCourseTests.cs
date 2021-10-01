using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Application.Courses.Commands.UpdateCourse;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Courses.Commands
{
    using static Testing;

    public class UpdateCourseTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCourseId()
        {
            var command = new UpdateCourseCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            var listId = await SendAsync(new CreateCourseCommand
            {
                Title = "New List"
            });

            await SendAsync(new CreateCourseCommand
            {
                Title = "Other List"
            });

            var command = new UpdateCourseCommand
            {
                Id = listId,
                Title = "Other List"
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                    .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title"))
                    .And.Errors["Title"].Should().Contain("The specified title already exists.");
        }

        [Test]
        public async Task ShouldUpdateCourse()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateCourseCommand
            {
                Title = "New List"
            });

            var command = new UpdateCourseCommand
            {
                Id = listId,
                Title = "Updated List Title"
            };

            await SendAsync(command);

            var list = await FindAsync<Course>(listId);

            list.Title.Should().Be(command.Title);
            list.LastModifiedBy.Should().NotBeNull();
            list.LastModifiedBy.Should().Be(userId);
            list.LastModified.Should().NotBeNull();
            list.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
