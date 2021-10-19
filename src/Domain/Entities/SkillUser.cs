using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    
    public class SkillUser : AuditableEntity
    {
        public int Id { get; set; }

        public SkillLevel Level { get; set; }

        public int UserId { get; set; }

        public int SkillId { get; set; }

        [ForeignKey(name: nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(name: nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}
