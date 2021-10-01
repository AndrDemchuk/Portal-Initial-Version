using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using System.Collections.Generic;

namespace BvAcademyPortal.Application.Courses.Queries.GetTodos
{
    public class CourseDto : IMapFrom<Course>
    {
        public CourseDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
