using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using System.Collections.Generic;

namespace BvAcademyPortal.Application.Courses.Queries.GetTodos
{
    public class CourseDto : IMapFrom<Course>
    {
        public CourseDto()
        {
            Items = new List<TopicDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TopicDto> Items { get; set; }
    }
}
