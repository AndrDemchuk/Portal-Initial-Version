using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Application.Courses.Queries.ExportTodos
{
    public class TopicRecord : IMapFrom<Topic>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
