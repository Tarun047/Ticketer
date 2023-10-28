using Microsoft.AspNetCore.Mvc;
using Ticketer.Business.Models;
using Ticketer.Business.Services.Ticket;

namespace Ticketer.WebApp.Controller;

[ApiController]
[Route("/api/events/{eventId:guid}/tickets")]
public class TicketController : ControllerBase
{
    readonly TicketService ticketService;

    public TicketController(TicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    [HttpGet]
    public IAsyncEnumerable<Ticket> GetAllTickets(Guid eventId)
    {
        return ticketService.GetTicketsForEvent(eventId);
    }

    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicketAsync(Guid eventId, CreateTicketRequest ticketCreationRequest)
    {
        var result = await ticketService.AddEventTicket(eventId, ticketCreationRequest);
        return result.Match<ActionResult<Ticket>>(
            ticket => Created("", ticket),
            err => BadRequest(err.First()));
    }
}