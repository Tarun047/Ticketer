using FluentValidation;
using Ticketer.Business.Models;

namespace Ticketer.Business.Validators;

public class EventAddressValidator : EventPresenceValidator<EventAddress>
{
    public EventAddressValidator(TicketerDbContext dbContext) : base(dbContext)
    {
        RuleFor(address => address.Id).NotEmpty();
        RuleFor(address => address.Address1).NotEmpty();
        RuleFor(address => address.City).NotEmpty();
        RuleFor(address => address.Country).Length(2);
    }
}