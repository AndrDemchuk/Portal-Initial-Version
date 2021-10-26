using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class Skill : BaseEntity<int>
    {
        public string Name { get; set; }

        public int SkillTypeId { get; set; }

        [ForeignKey(name: nameof(SkillTypeId))]
        public SkillType SkillType { get; set; }
        public ICollection<SkillUser> SkillsUsers { get; set; }
    }
}
