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

    public async Task<Result<PromptDto>> GetByIdAsync(int id)
    {
        var prompt = await unitOfWork.Prompts.GetByIdAsync(id);
        if (prompt == null)
            return Result<PromptDto>.Fail("Prompt Not Found");
        var dto = new PromptDto()
        {
            Id = prompt.Id,
            Title = prompt.Title,
            Description = prompt.Description,
            Price = prompt.Price
        };
        await unitOfWork.CompleteAsync();
        return Result<PromptDto>.Ok(dto);
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
    public async Task<Result<PromptDto>> UpdateAsync(int id,PromptDto promptDto)
    {
        var prompt = await unitOfWork.Prompts.GetByIdAsync(id);
        if (prompt == null)
            return Result<PromptDto>.Fail("Prompt Not Fount");
        prompt.Title = promptDto.Title;
        prompt.Description = promptDto.Description;
        prompt.Price = promptDto.Price;
        prompt.UpdatedDate = DateTime.Now;
        unitOfWork.Prompts.Update(prompt);
        await unitOfWork.CompleteAsync();
        return Result<PromptDto>.Ok(promptDto, "Prompt Update Successfully");
    }
    
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var prompt = await unitOfWork.Prompts.GetByIdAsync(id);
        if (prompt == null)
            return Result<bool>.Fail("Prompt Not Found");
        unitOfWork.Prompts.Remove(prompt);
        await unitOfWork.CompleteAsync();
        return Result<bool>.Ok(true, "Prompt Deleted");
    }
}