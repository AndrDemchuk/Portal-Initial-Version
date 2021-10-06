using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class CourseUsers : AuditableEntity
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdCourse { get; set; }

        public bool IsMentor { get; set; }

        [ForeignKey(name: "IdUser")]
        public virtual User user { get; set; }

        [ForeignKey(name: "IdCourse")]
        public virtual Course course { get; set; }
    }
}
