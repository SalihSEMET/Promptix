using Application.DTO_s;
using Application.Common;
namespace Application.Interfaces;

public interface IPromptService
{
    Task<Result<IEnumerable<PromptDto>>> GetAllAsync();
    Task<Result<PromptDto>> GetByIdAsync(int id);
    Task<Result<PromptDto>> CreateAsync(PromptDto promptDto);
    Task<Result<PromptDto>> UpdateAsync(int id,PromptDto promptDto);
    Task<Result<bool>> DeleteAsync(int id);
}