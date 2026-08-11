namespace TodoApp.Core.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}