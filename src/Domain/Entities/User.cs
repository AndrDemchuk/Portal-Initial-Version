using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BvAcademyPortal.Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        public string ProfilePhotoLink { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string EducationalEstablishment { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public string EnglishLevel { get; set; }

        public virtual ICollection<CourseUsers> courseUsers { get; set; }

        public virtual ICollection<SocialNetworkUsers> socialNetworkUsers { get; set; }

        public virtual ICollection<ProgrammingLanguage> programmingLanguage { get; set; }

    }
}
