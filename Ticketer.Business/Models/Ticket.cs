namespace Ticketer.Business.Models;

public class Ticket : IAuditableModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid EventId { get; set; }
    public string Tier { get; set; }
    public decimal BaseFare { get; set; }
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }
    public decimal Fare
    {
        get
        {
            var discountedFare = BaseFare * (100 - Discount) / 100;
            var finalFare = discountedFare * (100 + Tax) / 100;
            return finalFare;
        }
    }
}