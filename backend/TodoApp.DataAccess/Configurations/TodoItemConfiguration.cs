using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Core.Entities;

namespace TodoApp.DataAccess.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.HasKey(todoItem => todoItem.Id);

        builder.Property(todoItem => todoItem.Title)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(todoItem => todoItem.Description)
            .HasMaxLength(2048);

        builder.Property(todoItem => todoItem.IsCompleted)
            .IsRequired();

        builder.Property(todoItem => todoItem.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(todoItem => new
        {
            todoItem.UserId,
            todoItem.CreatedAtUtc
        });

        builder.HasIndex(todoItem => new
        {
            todoItem.UserId,
            todoItem.CategoryId
        });

        builder.HasOne(todoItem => todoItem.User)
            .WithMany(user => user.TodoItems)
            .HasForeignKey(todoItem => todoItem.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(todoItem => todoItem.Category)
            .WithMany(category => category.TodoItems)
            .HasForeignKey(todoItem => todoItem.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}