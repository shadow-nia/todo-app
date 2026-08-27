namespace TodoApp.Core.Models.TodoItems;

public sealed class TodoItemQuery
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public string? SearchTerm { get; init; }

    public Guid? CategoryId { get; init; }
}