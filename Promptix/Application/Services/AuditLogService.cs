using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuditLogService : IAuditLog
    {
        IUnitOfWork _unitOfWork;
        public AuditLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuditLog>> CreateAsync(AuditLog auditLog)
        {
            await _unitOfWork.AuditLogs.AddAsync(auditLog);
            await _unitOfWork.CompleteAsync();
            return Result<AuditLog>.Ok(auditLog, "Log Succesfully Created");
        }

        public async Task<Result<IEnumerable<AuditLog>>> GetAllAsync()
        {
            IEnumerable<AuditLog> logs = await _unitOfWork.AuditLogs.GetAllAsync();
            
            return Result<IEnumerable<AuditLog>>.Ok(logs, "Log Succesfully Created");
        }

        public async Task<Result<AuditLog>> GetByIdAsync(int id)
        {
            AuditLog log = await _unitOfWork.AuditLogs.GetByIdAsync(id);

            return Result<AuditLog>.Ok(log, "Log Succesfully Created");
        }
    }
}
