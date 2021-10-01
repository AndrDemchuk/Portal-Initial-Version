using AutoMapper;
using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Application.Courses.Queries.GetTodos
{
    public class TopicDto : IMapFrom<Topic>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Topic, TopicDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
