using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Result<IEnumerable<PurchaseDTO>>> GetAllAsync();
        Task<Result<PurchaseDTO>> GetByIdAsync(int id);
        Task<Result<PurchaseDTO>> CreateAsync(PurchaseDTO PurchaseDTO);
        Task<Result<PurchaseDTO>> UpdateAsync(int id, PurchaseDTO PurchaseDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
