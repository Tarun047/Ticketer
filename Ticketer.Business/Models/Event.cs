namespace Ticketer.Business.Models;

public class Event : IAuditableModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public DateTime Time { get; set; }
}