namespace Domain.Entities;

public class AuditLogType : BaseEntity
{
    public string? LogTypeName { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}