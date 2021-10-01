using FluentValidation;

namespace BvAcademyPortal.Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicCommandValidator : AbstractValidator<UpdateTopicCommand>
    {
        public UpdateTopicCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
