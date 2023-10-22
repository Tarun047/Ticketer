namespace Ticketer.Business.Models;

public class EventAddress : IModel
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
}