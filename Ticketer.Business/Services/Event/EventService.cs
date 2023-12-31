using FluentValidation;
using FluentValidation.Results;
using OneOf;
using Ticketer.Business.Models;

namespace Ticketer.Business.Services;

public class EventService : BaseService<Event>
{
    readonly IValidator<EventAddress> addressValidator;
    readonly IValidator<Event> eventValidator;

    public EventService(TicketerDbContext context, IValidator<Event> eventValidator,
        IValidator<EventAddress> addressValidator) : base(context)
    {
        this.eventValidator = eventValidator;
        this.addressValidator = addressValidator;
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

    public async Task<OneOf<EventAddress, IEnumerable<ValidationFailure>>> LinkToAddress(Guid eventId,
        AddressLinkRequest addressLink)
    {
        var eventAddress = new EventAddress
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Address1 = addressLink.Address1,
            Address2 = addressLink.Address2,
            City = addressLink.City,
            State = addressLink.State,
            PostCode = addressLink.PostCode,
            Country = addressLink.Country
        };

        var result = await addressValidator.ValidateAsync(eventAddress);

        if (result.IsValid) return (await DbContext.AddAsync(eventAddress)).Entity;

        return result.Errors;
    }
}