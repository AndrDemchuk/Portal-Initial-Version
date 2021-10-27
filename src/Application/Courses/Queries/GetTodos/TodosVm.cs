using System.Collections.Generic;

namespace BvAcademyPortal.Application.Courses.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; set; }

        public IList<CourseDto> Lists { get; set; }
    }
}
