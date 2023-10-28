using Microsoft.EntityFrameworkCore;
using Ticketer.Business.Models;

namespace Ticketer.Business;

public class TicketerDbContext : DbContext
{
    public TicketerDbContext(DbContextOptions<TicketerDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<EventAddress> EventAddresses { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }

    void AuditEntities()
    {
        var auditableModels = ChangeTracker.Entries<IAuditableModel>();
        foreach (var auditableModelEntry in auditableModels)
        {
            if (auditableModelEntry.State == EntityState.Added)
            {
                auditableModelEntry.Entity.CreatedAt = DateTime.UtcNow;
                auditableModelEntry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (auditableModelEntry.State == EntityState.Modified)
            {
                auditableModelEntry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    public override int SaveChanges()
    {
        AuditEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        AuditEntities();
        return base.SaveChangesAsync(cancellationToken);
    }
}