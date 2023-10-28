using Microsoft.AspNetCore.Mvc;
using Ticketer.Business.Services;

namespace Ticketer.WebApp.Controller;

[ApiController]
[Route("/api/reservations")]
public class ReservationController : ControllerBase
{
    readonly ReservationService reservationService;

    public ReservationController(ReservationService reservationService)
    {
        this.reservationService = reservationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Guid eventId, Guid ticketId)
    {
        var validationFailures = await reservationService.MakeReservation(eventId, ticketId);
        if (validationFailures.Any()) return BadRequest(validationFailures);

        return Ok();
    }
}