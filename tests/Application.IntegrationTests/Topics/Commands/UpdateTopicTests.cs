using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Topics.Commands.CreateTopic;
using BvAcademyPortal.Application.Topics.Commands.UpdateTopic;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace BvAcademyPortal.Application.IntegrationTests.Topics.Commands
{
    using static Testing;

    public class UpdateTopicTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTopicId()
        {
            var command = new UpdateTopicCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTopic()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateCourseCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTopicCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            var command = new UpdateTopicCommand
            {
                Id = itemId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<Topic>(itemId);

            item.Title.Should().Be(command.Title);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
