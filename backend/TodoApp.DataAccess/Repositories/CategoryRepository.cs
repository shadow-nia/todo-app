using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces.Repositories;
using TodoApp.DataAccess.Context;

namespace TodoApp.DataAccess.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Category>> GetAllByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(category => category.UserId == userId)
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Category?> GetByIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _context.Categories
            .FirstOrDefaultAsync(
                category =>
                    category.Id == id &&
                    category.UserId == userId,
                cancellationToken);
    }

    public Task<bool> ExistsByNameAsync(
        Guid userId,
        string name,
        Guid? excludedCategoryId = null,
        CancellationToken cancellationToken = default)
    {
        return _context.Categories
            .AnyAsync(
                category =>
                    category.UserId == userId &&
                    category.Name == name &&
                    (!excludedCategoryId.HasValue ||
                     category.Id != excludedCategoryId.Value),
                cancellationToken);
    }

    public void Add(Category category)
    {
        _context.Categories.Add(category);
    }

    public void Remove(Category category)
    {
        _context.Categories.Remove(category);
    }
}