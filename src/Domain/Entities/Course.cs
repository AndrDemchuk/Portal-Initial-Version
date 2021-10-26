using BvAcademyPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Domain.Entities
{
    public class Course: BaseEntity<int>
    {
        public string CourseName { get; set; }
        public string CoursePhotoLink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
    }
}
