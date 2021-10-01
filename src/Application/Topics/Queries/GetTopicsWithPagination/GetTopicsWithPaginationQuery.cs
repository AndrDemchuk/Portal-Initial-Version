using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Application.Common.Models;
using MediatR;

namespace BvAcademyPortal.Application.Topics.Queries.GetTopicsWithPagination
{
    public class GetTopicsWithPaginationQuery : IRequest<PaginatedList<TopicBriefDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTopicsWithPaginationQueryHandler : IRequestHandler<GetTopicsWithPaginationQuery, PaginatedList<TopicBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTopicsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TopicBriefDto>> Handle(GetTopicsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Topics
                .Where(x => x.ListId == request.ListId)
                .OrderBy(x => x.Title)
                .ProjectTo<TopicBriefDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
