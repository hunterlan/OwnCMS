using Microsoft.EntityFrameworkCore;
using OwnCMS.Application.Categories;
using OwnCMS.Entities;

namespace OwnCMS.Persistence.PostgreSQL.Categories;

/// <summary>
/// PostgreSQL-backed category data access for the application layer.
/// </summary>
public sealed class PostgreSqlCategoryDataAccess(OwnCmsDbContext dbContext) : ICategoryDataAccess
{
    public async Task<IReadOnlyList<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Category?> GetCategoryByNameOrSlugAsync(string name, string slug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));

        return dbContext.Categories
            .SingleOrDefaultAsync(
                category => category.Name == name || category.Slug == slug,
                cancellationToken);
    }

    public async Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(category);

        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }
}
