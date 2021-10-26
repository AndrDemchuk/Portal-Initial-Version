using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class SkillType : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
