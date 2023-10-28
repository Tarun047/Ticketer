using FluentValidation;
using Ticketer.Business.Models;

namespace Ticketer.Business.Validators;

public class ReservationValidator : EventPresenceValidator<Reservation>
{
    public ReservationValidator(TicketerDbContext dbContext) : base(dbContext)
    {
        RuleFor(reservation => reservation.TicketId).Must(BeAValidTicketId).WithMessage("No such ticket found");
    }

    bool BeAValidTicketId(Guid ticketId)
    {
        return DbContext.Find<Ticket>(ticketId) != null;
    }
}