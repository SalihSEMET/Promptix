namespace Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; } // null
    public bool IsActive { get; set; } = true;
}
// Users : User Information, roles, subscription status
// Prompts : Prompt content, category, description, price
// Categories: Prompt Categories ("Design","Software","Blog Content" and similar)
// Purchases : Which Prompt Did the User Purchase, Date and Payment Information
// Subscriptions : User's Subscription Type, Start and End Date
// Payments :Payment Records (mock or real gateway integration)
// Favorites : User Favorite Prompts
// AuditLogs : Logging Transaction History