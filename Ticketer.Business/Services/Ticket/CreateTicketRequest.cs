namespace Ticketer.Business.Services.Ticket;

public class CreateTicketRequest
{
    public string Tier { get; set; }
    public decimal BaseFare { get; set; }
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }
}