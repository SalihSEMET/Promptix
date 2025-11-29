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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<CategoryDTO>> CreateAsync(CategoryDTO CategoryDTO)
        {

            try
            {
                Category promt = _mapper.Map<Category>(CategoryDTO);

                await _unitOfWork.Categories.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                CategoryDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Categorys" });

                return Result<CategoryDTO>.Ok(CategoryDTO, "Category created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Categorys", UserAgent = ex.Message });
                return Result<CategoryDTO>.Fail("Category uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetByIdAsync(id);

                if (Category == null)
                    return Result<bool>.Fail("Category not found.");

                _unitOfWork.Categories.Remove(Category);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Category deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Categorys", UserAgent = ex.Message });
                return Result<bool>.Fail("Category undeleted.");
            }
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            try
            {
                var Categorys = await _unitOfWork.Categories.GetAllAsync();
                var result = _mapper.Map<IEnumerable<CategoryDTO>>(Categorys);
                return Result<IEnumerable<CategoryDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Categorys", UserAgent = ex.Message });
                return Result<IEnumerable<CategoryDTO>>.Fail("Category get exception.");
            }
        }

        public async Task<Result<CategoryDTO>> GetByIdAsync(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetByIdAsync(id);

                if (Category == null)
                    return Result<CategoryDTO>.Fail("Category not found.");

                CategoryDTO dto = _mapper.Map<CategoryDTO>(Category);
                return Result<CategoryDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Categorys", UserAgent = ex.Message });
                return Result<CategoryDTO>.Fail("Category get exception.");
            }

        }

        public async Task<Result<CategoryDTO>> UpdateAsync(int id, CategoryDTO CategoryDTO)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetByIdAsync(id);

                if (Category == null)
                    return Result<CategoryDTO>.Fail("Category not found");

                Category promt = _mapper.Map<Category>(CategoryDTO);

                _unitOfWork.Categories.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<CategoryDTO>.Ok(CategoryDTO, "Category updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Categorys", UserAgent = ex.Message });
                return Result<CategoryDTO>.Fail("Category un updated.");
            }
        }
    }
}
