using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPromptService
    {
        Task<Result<IEnumerable<PromptDTO>>> GetAllAsync();
        Task<Result<PromptDTO>> GetByIdAsync(int id);
        Task<Result<PromptDTO>> CreateAsync(PromptDTO promptDTO);
        Task<Result<PromptDTO>> UpdateAsync(int id,PromptDTO promptDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
