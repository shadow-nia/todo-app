namespace TodoApp.Core.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}