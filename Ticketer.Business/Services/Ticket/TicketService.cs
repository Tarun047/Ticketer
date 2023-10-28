using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Ticketer.Business.Services.Ticket;

public class TicketService : BaseService<Models.Ticket>
{
    readonly IValidator<Models.Ticket> ticketValidator;

    public TicketService(TicketerDbContext context, IValidator<Models.Ticket> ticketValidator) : base(context)
    {
        this.ticketValidator = ticketValidator;
    }

    public IAsyncEnumerable<Models.Ticket> GetTicketsForEvent(Guid eventId)
    {
        return DbContext.Tickets.Where(ticket => ticket.EventId == eventId).AsAsyncEnumerable();
    }

    public async Task<OneOf<Models.Ticket, IEnumerable<ValidationFailure>>> AddEventTicket(Guid eventId,
        CreateTicketRequest createTicketRequest)
    {
        var ticket = new Models.Ticket
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Tier = createTicketRequest.Tier,
            BaseFare = createTicketRequest.BaseFare,
            Tax = createTicketRequest.Tax,
            Discount = createTicketRequest.Discount
        };

        var result = await ticketValidator.ValidateAsync(ticket);
        if (result.IsValid) return await AddAsync(ticket);

        return result.Errors;
    }
}