namespace Ticketer.Business.Models;

public interface IEventLinked
{
    public Guid EventId { get; set; }
}