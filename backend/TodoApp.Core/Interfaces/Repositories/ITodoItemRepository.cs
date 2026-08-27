using TodoApp.Core.Entities;
using TodoApp.Core.Models.Pagination;
using TodoApp.Core.Models.TodoItems;

namespace TodoApp.Core.Interfaces.Repositories;

public interface ITodoItemRepository
{
    Task<PagedResult<TodoItem>> GetPagedAsync(
        Guid userId,
        TodoItemQuery query,
        CancellationToken cancellationToken = default);

    Task<TodoItem?> GetByIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default);

    void Add(TodoItem todoItem);

    void Remove(TodoItem todoItem);
}