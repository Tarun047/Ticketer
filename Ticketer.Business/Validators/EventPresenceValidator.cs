using FluentValidation;
using Ticketer.Business.Models;

namespace Ticketer.Business.Validators;

public abstract class EventPresenceValidator<T> : AbstractValidator<T> where T : IEventLinked
{
    protected readonly TicketerDbContext DbContext;

    protected EventPresenceValidator(TicketerDbContext dbContext)
    {
        DbContext = dbContext;
        RuleFor(eventLinkedEntity => eventLinkedEntity.EventId).Must(BeAValidEventId)
            .WithMessage("No such event found");
    }

    bool BeAValidEventId(Guid eventId)
    {
        return DbContext.Find<Event>(eventId) != null;
    }
}