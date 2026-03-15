using OwnCMS.Entities;

namespace OwnCMS.Application.Categories;

/// <summary>
/// Defines persistence-facing category data access required by the application layer.
/// </summary>
public interface ICategoryDataAccess
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of categories.</returns>
    Task<IReadOnlyList<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets a category by name or slug.
    /// </summary>
    /// <param name="name">The category name.</param>
    /// <param name="slug">The category slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The category if found; otherwise, <see langword="null" />.</returns>
    Task<Category?> GetCategoryByNameOrSlugAsync(string name, string slug, CancellationToken cancellationToken);

    /// <summary>
    /// Persists a new category.
    /// </summary>
    /// <param name="category">The category to persist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created category.</returns>
    Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
}
