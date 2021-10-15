using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUser
{
    public class CreateSkillUserCommandValidator: AbstractValidator<CreateSkillUserCommand>
    {
        public CreateSkillUserCommandValidator()
        {
            RuleFor(c => c.SkillId).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.SkillLevel).IsInEnum();
        }
    }
}
