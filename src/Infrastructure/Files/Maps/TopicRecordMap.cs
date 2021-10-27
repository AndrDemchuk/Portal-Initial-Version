using BvAcademyPortal.Application.Courses.Queries.ExportTodos;
using CsvHelper.Configuration;
using System.Globalization;

namespace BvAcademyPortal.Infrastructure.Files.Maps
{
    public class TopicRecordMap : ClassMap<TopicRecord>
    {
        public TopicRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
