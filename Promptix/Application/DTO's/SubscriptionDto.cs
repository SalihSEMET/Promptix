using Domain.Enums;

namespace Application.DTO_s;

public class SubscriptionDto
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public SubscriptionType Type { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
}