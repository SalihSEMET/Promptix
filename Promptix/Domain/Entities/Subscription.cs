namespace Domain.Entities;

public class Subscription : BaseEntity
{
    public int      AppUserId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    // Navigation Properties:
    // When establishing relationships between tables,
    // it allows us to specify which table has a relationship with which table and
    // to establish relationships between tables.
    public AppUser AppUser { get; set; }
    
}