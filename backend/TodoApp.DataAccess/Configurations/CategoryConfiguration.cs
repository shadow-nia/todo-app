using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Core.Entities;

namespace TodoApp.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(category => category.Id);
        
        builder.Property(category => category.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.HasIndex(category => new
            {
                category.UserId,
                category.Name
            })
            .IsUnique();
        
        builder.HasOne(category => category.User)
            .WithMany(user => user.Categories)
            .HasForeignKey(category => category.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}