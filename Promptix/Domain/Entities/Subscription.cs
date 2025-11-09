namespace Domain.Entities;

public class Subscription : BaseEntity
{
    public int      AppUserId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
}