using BvAcademyPortal.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCourseCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateCourseCommand model, string title, CancellationToken cancellationToken)
        {
            return await _context.Courses
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Title != title);
        }
    }
}
