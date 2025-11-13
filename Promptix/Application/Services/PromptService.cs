using Application.Common;
using Application.DTO_s;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PromptService(IUnitOfWork unitOfWork) : IPromptService
{
    public async Task<Result<IEnumerable<PromptDto>>> GetAllAsync()
    {
        var prompts = await unitOfWork.Prompts.GetAllAsync();
        var result = prompts.Select(p => new PromptDto
        {
            Id = p.Id,
            Price = p.Price,
            Title = p.Title,
            Description = p.Description
        });
        return Result<IEnumerable<PromptDto>>.Ok(result);
    }

    public Task<Result<PromptDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PromptDto>> CreateAsync(PromptDto promptDto)
    {
        var prompt = new Prompt
        {
            Title = promptDto.Title,
            Description = promptDto.Description,
            Price = promptDto.Price
        };
        await unitOfWork.Prompts.AddAsync(prompt);
        await unitOfWork.CompleteAsync();
        promptDto.Id = prompt.Id;
        return Result<PromptDto>.Ok(promptDto, "Prompt Created Successfully");
    }

    public Task<Result<PromptDto>> UpdateAsync(PromptDto promptDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PromptDto>> DeleteAsync(PromptDto promptDto)
    {
        throw new NotImplementedException();
    }
}