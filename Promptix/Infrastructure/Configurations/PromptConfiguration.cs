using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.ToTable("Prompts");

            builder.HasKey(x => x.Id); // Primary Key olarak tanımlamak için kullanılır.
            builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);

            builder.HasIndex(p => p.Title);

        }
    }
}
