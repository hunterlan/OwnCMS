using OwnCMS.Application.Categories.Models;

namespace OwnCMS.Application.Categories;

/// <summary>
/// Default application reader for category use cases.
/// </summary>
public sealed class CategoryReader(ICategoryDataAccess categoryDataAccess) : ICategoryReader
{
    public async Task<IReadOnlyList<CategorySummary>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await categoryDataAccess.GetCategoriesAsync(cancellationToken);

        return categories
            .Select(category => new CategorySummary(category.Id, category.Name, category.Slug))
            .OrderBy(category => category.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
