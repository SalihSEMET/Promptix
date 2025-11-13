using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PromptConfigurations : IEntityTypeConfiguration<Prompt>
{
    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.ToTable("Prompts");
        builder.HasKey(x => x.Id); // Has Primary Key
        builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Price).HasColumnName("decimal(18,2)").HasDefaultValue(0);
        builder.HasIndex(p => p.Title);
    }
}