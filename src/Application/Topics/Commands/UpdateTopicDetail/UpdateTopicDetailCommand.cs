using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Topics.Commands.UpdateTopicDetail
{
    public class UpdateTopicDetailCommand : IRequest
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTopicDetailCommandHandler : IRequestHandler<UpdateTopicDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTopicDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTopicDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Topics.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Topic), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
