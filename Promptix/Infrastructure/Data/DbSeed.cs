using Domain.Entities;

namespace Infrastructure.Data;

public static class DbSeed
{
    public static async Task SeedAsync(PromptixDbContext promptixDbContext)
    {
        if (promptixDbContext.Roles.Any())
        {
            promptixDbContext.Roles.AddRange(new[]
            {
                new AppRole { Name = "Admin", RoleType = Domain.Enums.RoleType.Admin },
                new AppRole { Name = "UserApp", RoleType = Domain.Enums.RoleType.UserApp }
            });
            await promptixDbContext.SaveChangesAsync();
        }

        if (!promptixDbContext.Categories.Any())
        {
            var cat = new Category { Name = "AI Prompts" };
            promptixDbContext.Categories.Add(cat);
            await promptixDbContext.SaveChangesAsync();

            var prompt = new Prompt
            {
                Title = "Email writer prompt",
                Description = "Professional prompt printing with AI",
                Content = "You are an export email writer...",
                Price = 4.99m,
            };

            promptixDbContext.Prompts.AddAsync(prompt);
            promptixDbContext.SaveChangesAsync();
            
            var pCat = new PromptCategory { PromptId = prompt.Id, CategoryId = cat.Id };
            promptixDbContext.PromptCategories.AddAsync(pCat);
            promptixDbContext.SaveChangesAsync();

        }
    }
}