using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Application.Courses.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
