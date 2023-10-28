using FluentValidation;
using FluentValidation.Results;
using Ticketer.Business.Models;

namespace Ticketer.Business.Services;

public class ReservationService : BaseService<Reservation>
{
    readonly TicketerDbContext dbContext;
    readonly IValidator<Reservation> reservationValidator;

    public ReservationService(TicketerDbContext dbContext, IValidator<Reservation> reservationValidator) :
        base(dbContext)
    {
        this.dbContext = dbContext;
        this.reservationValidator = reservationValidator;
    }

    public async Task<IEnumerable<ValidationFailure>> MakeReservation(Guid eventId, Guid ticketId)
    {
        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            TicketId = ticketId
        };

        var result = await reservationValidator.ValidateAsync(reservation);
        if (result.IsValid) await AddAsync(reservation);

        return result.Errors;
    }
}