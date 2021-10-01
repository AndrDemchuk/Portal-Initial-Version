using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Courses.Queries.ExportTodos;
using BvAcademyPortal.Infrastructure.Files.Maps;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BvAcademyPortal.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
