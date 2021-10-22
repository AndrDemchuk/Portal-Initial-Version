using AutoMapper;
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
            //else
            //{
            //    entity = _maper.Map<User>(request);
            //}


            entity.IsAdmin = request.IsAdmin;
            entity.ProfilePhotoLink = request.ProfilePhotoLink;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.BirthDate = request.BirthDate;
            entity.City = request.City;
            entity.Email = request.Email;
            entity.EducationalEstablishment = request.EducationalEstablishment;
            entity.Faculty = request.Faculty;
            entity.EnglishLevel = request.EnglishLevel;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
