
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>, IMapFrom<User>
    {
        public bool IsAdmin { get; set; }
        public string ProfilePhotoLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string EducationalEstablishment { get; set; }
        public string Faculty { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _maper;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper maper)
        {
            _context = context;
            _maper = maper;

        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _maper.Map<User>(request);

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
