using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditLogType:BaseEntity
    {
        public string LogTypeName { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}
