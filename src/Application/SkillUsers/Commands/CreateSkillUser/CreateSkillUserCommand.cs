using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUser
{
    public class CreateSkillUserCommand: IRequest<int>
    {
        public int SkillId { get; set; }
        public int UserId { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }

    public class CreateskillUserCommandHandler : IRequestHandler<CreateSkillUserCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateskillUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSkillUserCommand request, CancellationToken cancellationToken)
        {
            var skill = _context.Skills.Where(s => s.Id == request.SkillId);
            if(skill == null)
            {
                throw new NotFoundException(nameof(Skill), request.SkillId);
            }

            var entity = new SkillUser
            {
                UserId = request.UserId,
                SkillId = request.SkillId,
                Level = request.SkillLevel
            };

            await _context.SkillUsers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
