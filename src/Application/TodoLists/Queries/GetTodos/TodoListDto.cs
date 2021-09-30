using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using System.Collections.Generic;

namespace BvAcademyPortal.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
