using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillTypes.Queries
{
    public class SkillDto: IMapFrom<Skill>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
