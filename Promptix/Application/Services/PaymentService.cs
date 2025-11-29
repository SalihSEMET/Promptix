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
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<PaymentDTO>> CreateAsync(PaymentDTO PaymentDTO)
        {

            try
            {
                Payment promt = _mapper.Map<Payment>(PaymentDTO);

                await _unitOfWork.Payments.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                PaymentDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Payments" });

                return Result<PaymentDTO>.Ok(PaymentDTO, "Payment created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Payments", UserAgent = ex.Message });
                return Result<PaymentDTO>.Fail("Payment uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var Payment = await _unitOfWork.Payments.GetByIdAsync(id);

                if (Payment == null)
                    return Result<bool>.Fail("Payment not found.");

                _unitOfWork.Payments.Remove(Payment);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Payment deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Payments", UserAgent = ex.Message });
                return Result<bool>.Fail("Payment undeleted.");
            }
        }

        public async Task<Result<IEnumerable<PaymentDTO>>> GetAllAsync()
        {
            try
            {
                var Payments = await _unitOfWork.Payments.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PaymentDTO>>(Payments);
                return Result<IEnumerable<PaymentDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Payments", UserAgent = ex.Message });
                return Result<IEnumerable<PaymentDTO>>.Fail("Payment get exception.");
            }
        }

        public async Task<Result<PaymentDTO>> GetByIdAsync(int id)
        {
            try
            {
                var Payment = await _unitOfWork.Payments.GetByIdAsync(id);

                if (Payment == null)
                    return Result<PaymentDTO>.Fail("Payment not found.");

                PaymentDTO dto = _mapper.Map<PaymentDTO>(Payment);
                return Result<PaymentDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Payments", UserAgent = ex.Message });
                return Result<PaymentDTO>.Fail("Payment get exception.");
            }

        }

        public async Task<Result<PaymentDTO>> UpdateAsync(int id, PaymentDTO PaymentDTO)
        {
            try
            {
                var Payment = await _unitOfWork.Payments.GetByIdAsync(id);

                if (Payment == null)
                    return Result<PaymentDTO>.Fail("Payment not found");

                Payment promt = _mapper.Map<Payment>(PaymentDTO);

                _unitOfWork.Payments.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<PaymentDTO>.Ok(PaymentDTO, "Payment updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Payments", UserAgent = ex.Message });
                return Result<PaymentDTO>.Fail("Payment un updated.");
            }
        }
    }
}
