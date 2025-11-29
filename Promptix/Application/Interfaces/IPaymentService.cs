using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Result<IEnumerable<PaymentDTO>>> GetAllAsync();
        Task<Result<PaymentDTO>> GetByIdAsync(int id);
        Task<Result<PaymentDTO>> CreateAsync(PaymentDTO PaymentDTO);
        Task<Result<PaymentDTO>> UpdateAsync(int id, PaymentDTO PaymentDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
