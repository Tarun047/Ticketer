using Microsoft.AspNetCore.Mvc;
using Ticketer.Business.Models;
using Ticketer.Business.Services;

namespace Ticketer.WebApp.Controller;

[ApiController]
[Route("/api/events")]
public class EventController : ControllerBase
{
    readonly EventService eventService;

    public EventController(EventService eventService)
    {
        this.eventService = eventService;
    }


    [HttpGet]
    public IAsyncEnumerable<Event> GetAllEvents()
    {
        return eventService.GetAllEventsAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEventAsync(CreateEventRequest eventCreationRequest)
    {
        var result = await eventService.CreateEvent(eventCreationRequest);
        return result.Match<ActionResult<Event>>(
            evt => Created("", evt),
            err => BadRequest(err.First()));
    }
}