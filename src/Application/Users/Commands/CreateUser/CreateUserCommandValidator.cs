using BvAcademyPortal.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator:  AbstractValidator<CreateUserCommand>
    {
         private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.FirstName)
                   .NotEmpty().WithMessage("First name is required.")
                   .MaximumLength(200).WithMessage("First name must not exceed 200 characters.");
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(200).WithMessage("Last name must not exceed 200 characters.");
            RuleFor(v => v.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(200).WithMessage("City must not exceed 200 characters.");
            RuleFor(v => v.Email)
                 .NotEmpty().WithMessage("Email is required.")
                 .MaximumLength(200).WithMessage("Email must not exceed 200 characters.")
                 .EmailAddress().WithMessage("A valid email address is required.")
                 .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists."); ;
            RuleFor(v => v.EducationalEstablishment)
                .NotEmpty().WithMessage("EducationalEstablishment is required.")
                .MaximumLength(200).WithMessage("EducationalEstablishment must not exceed 200 characters.");
            RuleFor(v => v.Faculty)
                .NotEmpty().WithMessage("Faculty is required.")
                .MaximumLength(200).WithMessage("Faculty must not exceed 200 characters.");
            var greaterThan = new DateTime(1921, 1, 1);
            var lessThan = new DateTime(2008, 1, 1);
            RuleFor(v => v.BirthDate)
                .NotEmpty().WithMessage("Birthdate is required.")
                .InclusiveBetween(greaterThan, lessThan);
            RuleFor(v => v.EnglishLevel)
                .NotEmpty().WithMessage("Birthdate is required.");


        }       

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                    .AllAsync(l => l.Email != email);
        }


    }
}
