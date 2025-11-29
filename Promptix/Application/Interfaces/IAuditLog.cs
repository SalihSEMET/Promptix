using Application.Common;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuditLog
    {
        Task<Result<IEnumerable<AuditLog>>> GetAllAsync();
        Task<Result<AuditLog>> GetByIdAsync(int id);
        Task<Result<AuditLog>> CreateAsync(AuditLog promptDTO);
    }
}
