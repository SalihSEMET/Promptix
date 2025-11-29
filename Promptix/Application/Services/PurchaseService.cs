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
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<PurchaseDTO>> CreateAsync(PurchaseDTO PurchaseDTO)
        {

            try
            {
                Purchase promt = _mapper.Map<Purchase>(PurchaseDTO);

                await _unitOfWork.Purchases.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                PurchaseDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Purchases" });

                return Result<PurchaseDTO>.Ok(PurchaseDTO, "Purchase created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Purchases", UserAgent = ex.Message });
                return Result<PurchaseDTO>.Fail("Purchase uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var Purchase = await _unitOfWork.Purchases.GetByIdAsync(id);

                if (Purchase == null)
                    return Result<bool>.Fail("Purchase not found.");

                _unitOfWork.Purchases.Remove(Purchase);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Purchase deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Purchases", UserAgent = ex.Message });
                return Result<bool>.Fail("Purchase undeleted.");
            }
        }

        public async Task<Result<IEnumerable<PurchaseDTO>>> GetAllAsync()
        {
            try
            {
                var Purchases = await _unitOfWork.Purchases.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PurchaseDTO>>(Purchases);
                return Result<IEnumerable<PurchaseDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Purchases", UserAgent = ex.Message });
                return Result<IEnumerable<PurchaseDTO>>.Fail("Purchase get exception.");
            }
        }

        public async Task<Result<PurchaseDTO>> GetByIdAsync(int id)
        {
            try
            {
                var Purchase = await _unitOfWork.Purchases.GetByIdAsync(id);

                if (Purchase == null)
                    return Result<PurchaseDTO>.Fail("Purchase not found.");

                PurchaseDTO dto = _mapper.Map<PurchaseDTO>(Purchase);
                return Result<PurchaseDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Purchases", UserAgent = ex.Message });
                return Result<PurchaseDTO>.Fail("Purchase get exception.");
            }

        }

        public async Task<Result<PurchaseDTO>> UpdateAsync(int id, PurchaseDTO PurchaseDTO)
        {
            try
            {
                var Purchase = await _unitOfWork.Purchases.GetByIdAsync(id);

                if (Purchase == null)
                    return Result<PurchaseDTO>.Fail("Purchase not found");

                Purchase promt = _mapper.Map<Purchase>(PurchaseDTO);

                _unitOfWork.Purchases.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<PurchaseDTO>.Ok(PurchaseDTO, "Purchase updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Purchases", UserAgent = ex.Message });
                return Result<PurchaseDTO>.Fail("Purchase un updated.");
            }
        }
    }
}
