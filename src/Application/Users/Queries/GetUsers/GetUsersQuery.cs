using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Queries.GetTodos
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {

    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = new List<UserDto>();
            users = await _context.Users
                    .AsNoTracking()
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.FirstName)
                    .ToListAsync(cancellationToken);
            return users;
        }
    }
}
