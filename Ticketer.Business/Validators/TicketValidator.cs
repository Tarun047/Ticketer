using FluentValidation;
using Ticketer.Business.Models;

namespace Ticketer.Business.Validators;

public class TicketValidator : EventPresenceValidator<Ticket>
{
    public TicketValidator(TicketerDbContext dbContext) : base(dbContext)
    {
        RuleFor(ticket => ticket.Tier).NotEmpty();
        RuleFor(ticket => ticket.Fare).GreaterThanOrEqualTo(0);
        RuleFor(ticket => ticket.Discount).InclusiveBetween(0, 100);
        RuleFor(ticket => ticket.Tax).InclusiveBetween(0, 100);
    }
}