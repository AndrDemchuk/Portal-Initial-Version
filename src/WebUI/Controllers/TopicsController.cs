using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Application.Topics.Commands.CreateTopic;
using BvAcademyPortal.Application.Topics.Commands.DeleteTopic;
using BvAcademyPortal.Application.Topics.Commands.UpdateTopic;
using BvAcademyPortal.Application.Topics.Commands.UpdateTopicDetail;
using BvAcademyPortal.Application.Topics.Queries.GetTopicsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    [Authorize]
    public class TopicsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<TopicBriefDto>>> GetTopicsWithPagination([FromQuery] GetTopicsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTopicCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTopicCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTopicDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTopicCommand { Id = id });

            return NoContent();
        }
    }
}
