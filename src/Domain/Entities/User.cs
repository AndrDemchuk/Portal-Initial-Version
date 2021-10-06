using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;

namespace BvAcademyPortal.Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string SkypeName { get; set; }

        public string EducationalEstablishment { get; set; }

        public string Faculty { get; set; }

        public string EnglishLevel { get; set; }

        public virtual ICollection<CourseUsers> CourseUsers { get; set; }
    }
}
