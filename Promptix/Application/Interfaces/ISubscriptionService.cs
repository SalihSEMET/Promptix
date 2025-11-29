using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<Result<IEnumerable<SubscriptionDTO>>> GetAllAsync();
        Task<Result<SubscriptionDTO>> GetByIdAsync(int id);
        Task<Result<SubscriptionDTO>> CreateAsync(SubscriptionDTO SubscriptionDTO);
        Task<Result<SubscriptionDTO>> UpdateAsync(int id, SubscriptionDTO SubscriptionDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
