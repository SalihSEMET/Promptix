using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser<int>
{
    public string FirstNameLastName { get; set; } = "";

    public string Address { get; set; } = "";
    // Navigation Properties
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
    
}