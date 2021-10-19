using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.SkillTypes.Queries
{
    public class GetSkillTypesQuery: IRequest<IReadOnlyCollection<SkillTypeDto>>
    {
    }

    public class GetSkillTypesQueryHandler : IRequestHandler<GetSkillTypesQuery, IReadOnlyCollection<SkillTypeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSkillTypesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SkillTypeDto>> Handle(GetSkillTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.SkillTypes
                .AsNoTracking()
                .ProjectTo<SkillTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
