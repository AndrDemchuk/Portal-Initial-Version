using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BvAcademyPortal.Domain.Entities
{
    public class ProgrammingLanguage : AuditableEntity
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public string Level { get; set; }

        public virtual ICollection<ProgrammingLanguage> programmingLanguage { get; set; }
    }
}
