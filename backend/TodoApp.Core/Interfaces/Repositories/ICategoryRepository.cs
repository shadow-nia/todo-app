using TodoApp.Core.Entities;

namespace TodoApp.Core.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAllByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<Category?> GetByIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        Guid userId,
        string name,
        Guid? excludedCategoryId = null,
        CancellationToken cancellationToken = default);

    void Add(Category category);

    void Remove(Category category);
}