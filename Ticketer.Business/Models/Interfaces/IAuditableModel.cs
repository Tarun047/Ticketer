namespace Ticketer.Business.Models;

public interface IAuditableModel : IModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}