using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace BvAcademyPortal.Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; }
        public string CoursePhotoLink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CourseUsers> CourseUsers { get; set; }
    }
}
