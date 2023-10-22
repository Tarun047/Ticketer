namespace Ticketer.Business.Services;

public class CreateEventRequest
{
    public string Name { get; set; }
    public DateTimeOffset ScheduledAt { get; set; }
}