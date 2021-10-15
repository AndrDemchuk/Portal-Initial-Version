using BvAcademyPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers
{
    public class SkillUserCreationDto
    {
        public int SkillId { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
