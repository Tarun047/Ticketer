using FluentValidation;
using Ticketer.Business.Models;

namespace Ticketer.Business.Validators;

public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
    {
        RuleFor(evt => evt.Name).NotEmpty();
        RuleFor(evt => evt.Time).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Event Can't be scheduled in past");
    }
}