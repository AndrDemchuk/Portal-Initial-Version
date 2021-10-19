using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers
{
    public class CreateSkillUsersCommand: IRequest<List<int>>
    {
        public IReadOnlyCollection<SkillUserCreationDto> UserSkills { get; set; }
        public string UserId { get; set; }
    }

    public class CreateSkillUsersCommandHandler : IRequestHandler<CreateSkillUsersCommand, List<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateSkillUsersCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> Handle(CreateSkillUsersCommand request, CancellationToken cancellationToken)
        {
            List<int> ids = new();
            IReadOnlyCollection<int> skillsIds = await _context.Skills.AsNoTracking()
                                                                        .Select(s => s.Id)
                                                                        .ToListAsync();

            var userId = int.Parse(request.UserId);
            var user = await _context.Users.FindAsync(userId);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            foreach(var skillUser in request.UserSkills)
            {
                if(!skillsIds.Contains(skillUser.SkillId))
                {
                    throw new NotFoundException(nameof(Skill), skillUser.SkillId);
                }

                var entity = new SkillUser
                {
                    SkillId = skillUser.SkillId,
                    UserId = userId,
                    Level = skillUser.SkillLevel
                };

                await _context.SkillUsers.AddAsync(entity, cancellationToken);
                ids.Add(entity.Id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ids;
        }
    }
}
