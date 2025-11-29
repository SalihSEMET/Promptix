using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class AuditLog : BaseEntity
{
    public int AppUserId { get; set; }
    public LogType LogTypeId { get; set; } = LogType.Success;
    public string TableName { get; set; } = "";
    public string? RecordId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    //Navigation Properties
    public AppUser AppUser { get; set; }
    [ForeignKey("LogTypeId")]
    public AuditLogType AuditLogType { get; set; }
}