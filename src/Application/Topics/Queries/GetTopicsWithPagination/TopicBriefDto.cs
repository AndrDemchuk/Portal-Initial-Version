using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Application.Topics.Queries.GetTopicsWithPagination
{
    public class TopicBriefDto : IMapFrom<Topic>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
