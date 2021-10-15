using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers
{
    public class CreateSkillUsersCommandValidator: AbstractValidator<CreateSkillUsersCommand>
    {
        public CreateSkillUsersCommandValidator()
        {
            RuleFor(c => c.List).NotEmpty().DependentRules(() => 
            {
                RuleForEach(c => c.List).SetValidator(new SkillUserCreationDtoValidator());
            });
            RuleFor(c => c.UserId).NotEmpty().DependentRules(() =>
            {
                RuleFor(c => c.UserId).Must(id => int.TryParse(id, out int _)).WithMessage("Invalid user id");
            });
        }
    }

    public class SkillUserCreationDtoValidator: AbstractValidator<SkillUserCreationDto>
    {
        public SkillUserCreationDtoValidator()
        {
            RuleFor(s => s.SkillLevel).IsInEnum();
            RuleFor(s => s.SkillId).NotEmpty();
        }
    }
}
