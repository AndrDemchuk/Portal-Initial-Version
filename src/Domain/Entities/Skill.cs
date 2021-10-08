using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class Skill : AuditableEntity
    {
        public int Id { get; set; }

        public int SkillTypeId { get; set; }

        [ForeignKey(name: "SkillTypeId")]
        public SkillType SkillType { get; set; }
    }
}
