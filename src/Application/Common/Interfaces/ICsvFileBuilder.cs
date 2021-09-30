using BvAcademyPortal.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
