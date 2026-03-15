using OwnCMS.Application.Categories.Models;

namespace OwnCMS.Application.Categories;

/// <summary>
/// Provides read operations for category-related application use cases.
/// </summary>
public interface ICategoryReader
{
    /// <summary>
    /// Gets all categories available to the caller.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of category summaries.</returns>
    Task<IReadOnlyList<CategorySummary>> GetCategoriesAsync(CancellationToken cancellationToken);
}
