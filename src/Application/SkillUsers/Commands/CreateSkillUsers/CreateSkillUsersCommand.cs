using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers
{
    public class CreateSkillUsersCommand: IRequest<List<int>>
    {
        public IEnumerable<SkillUserCreationDto> List { get; set; }
        public string UserId { get; set; }
    }

    public class CreateSkillUsersCommandHandler : IRequestHandler<CreateSkillUsersCommand, List<int>>
    {
        private readonly IApplicationDbContext _context;

        public async Task<List<int>> Handle(CreateSkillUsersCommand request, CancellationToken cancellationToken)
        {
            List<int> ids = new();

            var userId = int.Parse(request.UserId);
            var user = _context.Users.Where(u => u.Id == userId);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            foreach(var c in request.List)
            {
                var skill = _context.Skills.Where(s => s.Id == c.SkillId).SingleOrDefault();

                if(skill == null)
                {
                    throw new NotFoundException(nameof(Skill), c.SkillId);
                }

                var entity = new SkillUser
                {
                    SkillId = c.SkillId,
                    UserId = userId,
                    Level = c.SkillLevel
                };

                await _context.SkillUsers.AddAsync(entity, cancellationToken);
                ids.Add(entity.Id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ids;
        }
    }
}
