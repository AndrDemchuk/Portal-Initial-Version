using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Exceptions;
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
    public class GetUserQuery:IRequest<GetUserVm>
    {
        public string UserId { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.UserId);

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }
            
            return new GetUserVm
            {
                InformationsOfUser = await _context.Users
                    .Where(p=>p.Id.Equals(userId))
                    .Select(p => new UserShortDto { ProfilePhotoLink = p.ProfilePhotoLink, FirstName = p.FirstName, LastName = p.LastName, Email = p.Email })
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
