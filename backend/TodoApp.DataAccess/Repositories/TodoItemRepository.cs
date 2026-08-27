using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces.Repositories;
using TodoApp.Core.Models.Pagination;
using TodoApp.Core.Models.TodoItems;
using TodoApp.DataAccess.Context;

namespace TodoApp.DataAccess.Repositories;

public sealed class TodoItemRepository : ITodoItemRepository
{
    private readonly AppDbContext _context;

    public TodoItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TodoItem>> GetPagedAsync(
        Guid userId,
        TodoItemQuery query,
        CancellationToken cancellationToken = default)
    {
        var todoItemsQuery = _context.TodoItems
            .AsNoTracking()
            .Where(todoItem => todoItem.UserId == userId);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var searchPattern = $"%{query.SearchTerm.Trim()}%";

            todoItemsQuery = todoItemsQuery.Where(todoItem =>
                EF.Functions.ILike(todoItem.Title, searchPattern) ||
                (todoItem.Description != null &&
                 EF.Functions.ILike(
                     todoItem.Description,
                     searchPattern)));
        }

        if (query.CategoryId.HasValue)
        {
            todoItemsQuery = todoItemsQuery.Where(todoItem =>
                todoItem.CategoryId == query.CategoryId.Value);
        }

        var totalCount = await todoItemsQuery
            .CountAsync(cancellationToken);

        var items = await todoItemsQuery
            .Include(todoItem => todoItem.Category)
            .OrderByDescending(todoItem => todoItem.CreatedAtUtc)
            .ThenByDescending(todoItem => todoItem.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TodoItem>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public Task<TodoItem?> GetByIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _context.TodoItems
            .FirstOrDefaultAsync(
                todoItem =>
                    todoItem.Id == id &&
                    todoItem.UserId == userId,
                cancellationToken);
    }

    public void Add(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
    }

    public void Remove(TodoItem todoItem)
    {
        _context.TodoItems.Remove(todoItem);
    }
}