namespace Domain.Entities;

public class AuditLog : BaseEntity
{
    public int AppUserId { get; set; }
    public string Action { get; set; } = "";
    public string TableName { get; set; } = "";
    public string? RecordId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    
}