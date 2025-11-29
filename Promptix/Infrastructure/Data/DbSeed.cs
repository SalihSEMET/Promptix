using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
    public static class DbSeed
    {
        public static async Task SeedAsync(PromptixDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(new[]
                {
                    new AppRole{Name = "Admin",RoleType = Domain.Enums.RoleType.Admin},
                    new AppRole{Name = "UserApp",RoleType = Domain.Enums.RoleType.UserApp}
                });

                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Categories.Any())
            {
                var cat = new Category { Name = "AI Prompts" };
                dbContext.Categories.Add(cat);
                await dbContext.SaveChangesAsync();

                var prompt = new Prompt
                {
                    Title = "Email writer prompt",
                    Description = "AI ile profesyonel e-posta yazdırma promptu.",
                    Content = "You are an export email writer...",
                    Price = 4.99m
                };

                dbContext.Prompts.AddAsync(prompt);
                dbContext.SaveChangesAsync();


                var pCat = new PromptCategory { PromptId = prompt.Id, CategoryId = cat.Id };
                dbContext.PromptCategories.AddAsync(pCat);
                dbContext.SaveChangesAsync();
            }
        }
    }
}
