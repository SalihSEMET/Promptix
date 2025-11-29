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
    public class PromptCategoryService : IPromptCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public PromptCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<PromptCategoryDTO>> CreateAsync(PromptCategoryDTO PromptCategoryDTO)
        {

            try
            {
                PromptCategory promt = _mapper.Map<PromptCategory>(PromptCategoryDTO);

                await _unitOfWork.PromptCategories.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                PromptCategoryDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "PromptCategorys" });

                return Result<PromptCategoryDTO>.Ok(PromptCategoryDTO, "PromptCategory created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "PromptCategorys", UserAgent = ex.Message });
                return Result<PromptCategoryDTO>.Fail("PromptCategory uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var PromptCategory = await _unitOfWork.PromptCategories.GetByIdAsync(id);

                if (PromptCategory == null)
                    return Result<bool>.Fail("PromptCategory not found.");

                _unitOfWork.PromptCategories.Remove(PromptCategory);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "PromptCategory deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "PromptCategorys", UserAgent = ex.Message });
                return Result<bool>.Fail("PromptCategory undeleted.");
            }
        }

        public async Task<Result<IEnumerable<PromptCategoryDTO>>> GetAllAsync()
        {
            try
            {
                var PromptCategorys = await _unitOfWork.PromptCategories.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PromptCategoryDTO>>(PromptCategorys);
                return Result<IEnumerable<PromptCategoryDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "PromptCategorys", UserAgent = ex.Message });
                return Result<IEnumerable<PromptCategoryDTO>>.Fail("PromptCategory get exception.");
            }
        }

        public async Task<Result<PromptCategoryDTO>> GetByIdAsync(int id)
        {
            try
            {
                var PromptCategory = await _unitOfWork.PromptCategories.GetByIdAsync(id);

                if (PromptCategory == null)
                    return Result<PromptCategoryDTO>.Fail("PromptCategory not found.");

                PromptCategoryDTO dto = _mapper.Map<PromptCategoryDTO>(PromptCategory);
                return Result<PromptCategoryDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "PromptCategorys", UserAgent = ex.Message });
                return Result<PromptCategoryDTO>.Fail("PromptCategory get exception.");
            }

        }

        public async Task<Result<PromptCategoryDTO>> UpdateAsync(int id, PromptCategoryDTO PromptCategoryDTO)
        {
            try
            {
                var PromptCategory = await _unitOfWork.PromptCategories.GetByIdAsync(id);

                if (PromptCategory == null)
                    return Result<PromptCategoryDTO>.Fail("PromptCategory not found");

                PromptCategory promt = _mapper.Map<PromptCategory>(PromptCategoryDTO);

                _unitOfWork.PromptCategories.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<PromptCategoryDTO>.Ok(PromptCategoryDTO, "PromptCategory updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "PromptCategorys", UserAgent = ex.Message });
                return Result<PromptCategoryDTO>.Fail("PromptCategory un updated.");
            }
        }
    }
}
