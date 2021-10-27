using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Topics.Commands.CreateTopic;
using BvAcademyPortal.Application.Topics.Commands.DeleteTopic;
using BvAcademyPortal.Application.Courses.Commands.CreateCourse;
using BvAcademyPortal.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BvAcademyPortal.Application.IntegrationTests.Topics.Commands
{
    using static Testing;

    public class DeleteTopicTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTopicId()
        {
            var command = new DeleteTopicCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTopic()
        {
            var listId = await SendAsync(new CreateCourseCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTopicCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTopicCommand
            {
                Id = itemId
            });

            var item = await FindAsync<Topic>(itemId);

            item.Should().BeNull();
        }
    }
}
