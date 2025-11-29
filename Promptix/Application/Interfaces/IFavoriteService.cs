using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<Result<IEnumerable<FavoriteDTO>>> GetAllAsync();
        Task<Result<FavoriteDTO>> GetByIdAsync(int id);
        Task<Result<FavoriteDTO>> CreateAsync(FavoriteDTO FavoriteDTO);
        Task<Result<FavoriteDTO>> UpdateAsync(int id, FavoriteDTO FavoriteDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
