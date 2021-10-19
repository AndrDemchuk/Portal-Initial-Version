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
    public class GetUserQuery:IRequest<UserShortDto>
    {
        public string UserId { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserShortDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserShortDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.UserId);

            var user = await _context.Users.FindAsync(userId);
            
            
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var userInfo = new UserShortDto();
            userInfo.ProfilePhotoLink = user.ProfilePhotoLink;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.Email = user.Email;
            return userInfo;
        }
    }
}
