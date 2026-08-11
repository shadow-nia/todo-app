namespace TodoApp.Core.Entities;

public class TodoItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;

    public Guid? CategoryId { get; set; }
    
    public Category? Category { get; set; }
}