using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPromptCategoryService
    {
        Task<Result<IEnumerable<PromptCategoryDTO>>> GetAllAsync();
        Task<Result<PromptCategoryDTO>> GetByIdAsync(int id);
        Task<Result<PromptCategoryDTO>> CreateAsync(PromptCategoryDTO PromptCategoryDTO);
        Task<Result<PromptCategoryDTO>> UpdateAsync(int id, PromptCategoryDTO PromptCategoryDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
