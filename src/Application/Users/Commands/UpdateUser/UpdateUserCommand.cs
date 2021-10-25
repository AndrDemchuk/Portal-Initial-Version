﻿using AutoMapper;
using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest, IMapFrom<User>
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string EducationalEstablishment { get; set; }
        public string Faculty { get; set; }
        public EnglishLevel EnglishLevel { get; set; }

        public bool IsDeactivated { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _maper;

        public UpdateUserCommandHandler(IApplicationDbContext context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            else
            {
                _maper.Map<UpdateUserCommand, User>(request, entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
