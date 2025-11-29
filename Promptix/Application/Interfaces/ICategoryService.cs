using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryDTO>>> GetAllAsync();
        Task<Result<CategoryDTO>> GetByIdAsync(int id);
        Task<Result<CategoryDTO>> CreateAsync(CategoryDTO CategoryDTO);
        Task<Result<CategoryDTO>> UpdateAsync(int id, CategoryDTO CategoryDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
