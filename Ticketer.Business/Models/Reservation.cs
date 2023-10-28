namespace Ticketer.Business.Models;

public class Reservation : IAuditableModel, IEventLinked
{
    public Guid TicketId { get; set; }
    public bool IsCancelled { get; set; }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid EventId { get; set; }
}