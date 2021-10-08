using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    
    public class SkillsUsers : AuditableEntity
    {
        public int Id { get; set; }

        public SkillLevel Level { get; set; }

        public int UserId { get; set; }

        public int SkillId { get; set; }

        [ForeignKey(name: "UserId")]
        public virtual User user { get; set; }

        [ForeignKey(name: "SkillId")]
        public virtual Skill Skill { get; set; }
    }
}
