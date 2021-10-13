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
    public class GetUsersQuery : IRequest<UsersVm>
    {

    }
    public class GetTodosQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return new UsersVm
            {
                Lists = await _context.Users
                    .AsNoTracking()
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.FirstName)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
