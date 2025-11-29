using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public SubscriptionService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<SubscriptionDTO>> CreateAsync(SubscriptionDTO SubscriptionDTO)
        {

            try
            {
                Subscription promt = _mapper.Map<Subscription>(SubscriptionDTO);

                await _unitOfWork.Subscriptions.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                SubscriptionDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Subscriptions" });

                return Result<SubscriptionDTO>.Ok(SubscriptionDTO, "Subscription created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Subscriptions", UserAgent = ex.Message });
                return Result<SubscriptionDTO>.Fail("Subscription uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var Subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);

                if (Subscription == null)
                    return Result<bool>.Fail("Subscription not found.");

                _unitOfWork.Subscriptions.Remove(Subscription);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Subscription deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Subscriptions", UserAgent = ex.Message });
                return Result<bool>.Fail("Subscription undeleted.");
            }
        }

        public async Task<Result<IEnumerable<SubscriptionDTO>>> GetAllAsync()
        {
            try
            {
                var Subscriptions = await _unitOfWork.Subscriptions.GetAllAsync();
                var result = _mapper.Map<IEnumerable<SubscriptionDTO>>(Subscriptions);
                return Result<IEnumerable<SubscriptionDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Subscriptions", UserAgent = ex.Message });
                return Result<IEnumerable<SubscriptionDTO>>.Fail("Subscription get exception.");
            }
        }

        public async Task<Result<SubscriptionDTO>> GetByIdAsync(int id)
        {
            try
            {
                var Subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);

                if (Subscription == null)
                    return Result<SubscriptionDTO>.Fail("Subscription not found.");

                SubscriptionDTO dto = _mapper.Map<SubscriptionDTO>(Subscription);
                return Result<SubscriptionDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Subscriptions", UserAgent = ex.Message });
                return Result<SubscriptionDTO>.Fail("Subscription get exception.");
            }

        }

        public async Task<Result<SubscriptionDTO>> UpdateAsync(int id, SubscriptionDTO SubscriptionDTO)
        {
            try
            {
                var Subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);

                if (Subscription == null)
                    return Result<SubscriptionDTO>.Fail("Subscription not found");

                Subscription promt = _mapper.Map<Subscription>(SubscriptionDTO);

                _unitOfWork.Subscriptions.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<SubscriptionDTO>.Ok(SubscriptionDTO, "Subscription updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Subscriptions", UserAgent = ex.Message });
                return Result<SubscriptionDTO>.Fail("Subscription un updated.");
            }
        }
    }
}
