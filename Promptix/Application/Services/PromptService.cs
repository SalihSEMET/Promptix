using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Prompt = Domain.Entities.Prompt;

namespace Application.Services
{
    public class PromptService : IPromptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public PromptService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<PromptDTO>> CreateAsync(PromptDTO promptDTO)
        {

            try
            {
                Prompt promt = _mapper.Map<Prompt>(promptDTO);

                await _unitOfWork.Prompts.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                promptDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Prompts" });

                return Result<PromptDTO>.Ok(promptDTO, "Prompt created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Prompts",UserAgent=ex.Message });
                return Result<PromptDTO>.Fail("Prompt uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var prompt = await _unitOfWork.Prompts.GetByIdAsync(id);

                if (prompt == null)
                    return Result<bool>.Fail("Prompt not found.");

                _unitOfWork.Prompts.Remove(prompt);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Prompt deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Prompts", UserAgent = ex.Message });
                return Result<bool>.Fail("Prompt undeleted.");
            }
        }

        public async Task<Result<IEnumerable<PromptDTO>>> GetAllAsync()
        {
            try
            {
                var prompts = await _unitOfWork.Prompts.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PromptDTO>>(prompts);
                return Result<IEnumerable<PromptDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Prompts", UserAgent = ex.Message });
                return Result<IEnumerable<PromptDTO>>.Fail("Prompt get exception.");
            }
        }

        public async Task<Result<PromptDTO>> GetByIdAsync(int id)
        {
            try
            {
                var prompt = await _unitOfWork.Prompts.GetByIdAsync(id);

                if (prompt == null)
                    return Result<PromptDTO>.Fail("Prompt not found.");

                PromptDTO dto = _mapper.Map<PromptDTO>(prompt);
                return Result<PromptDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Prompts", UserAgent = ex.Message });
                return Result<PromptDTO>.Fail("Prompt get exception.");
            }

        }

        public async Task<Result<PromptDTO>> UpdateAsync(int id,PromptDTO promptDTO)
        {
            try
            {
                var prompt = await _unitOfWork.Prompts.GetByIdAsync(id);

                if (prompt == null)
                    return Result<PromptDTO>.Fail("Prompt not found");

                Prompt promt = _mapper.Map<Prompt>(promptDTO);

                _unitOfWork.Prompts.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<PromptDTO>.Ok(promptDTO, "Prompt updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Prompts", UserAgent = ex.Message });
                return Result<PromptDTO>.Fail("Prompt un updated.");
            }
        }
    }
}
