using BvAcademyPortal.Application.Courses.Queries.ExportTodos;
using CsvHelper.Configuration;
using System.Globalization;

namespace BvAcademyPortal.Infrastructure.Files.Maps
{
    public class TodoItemRecordMap : ClassMap<TodoItemRecord>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
