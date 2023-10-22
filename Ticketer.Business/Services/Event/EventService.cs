using FluentValidation;
using FluentValidation.Results;
using OneOf;
using Ticketer.Business.Models;
using Ticketer.Business.Services.Errors;

namespace Ticketer.Business.Services;

public class EventService : BaseService<Event>
{
    readonly IValidator<Event> eventValidator;
    public EventService(TicketerDbContext context, IValidator<Event> eventValidator) : base(context)
    {
        this.eventValidator = eventValidator;
    }

    public IAsyncEnumerable<Event> GetAllEventsAsync()
    {
        return DbContext.Events.AsAsyncEnumerable();
    }

    public async Task<OneOf<Event, IEnumerable<ValidationFailure>>> CreateEvent(CreateEventRequest createEventRequest)
    {
        var evt = new Event
        {
            Id = Guid.NewGuid(),
            Name = createEventRequest.Name,
            Time = createEventRequest.ScheduledAt.UtcDateTime
        }; 
        
        var result = await eventValidator.ValidateAsync(evt);

        if (result.IsValid)
        {
            return await AddAsync(evt);
        }

        return result.Errors;
    }
}