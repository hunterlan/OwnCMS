using OwnCMS.Application.Categories.Models;

namespace OwnCMS.Application.Articles.Models;

/// <summary>
/// Represents a lightweight article view for list scenarios.
/// </summary>
/// <param name="Id">The article identifier.</param>
/// <param name="Name">The article name.</param>
/// <param name="Slug">The article slug.</param>
/// <param name="CreatedAt">The article creation timestamp.</param>
/// <param name="UpdatedAt">The article update timestamp.</param>
/// <param name="Categories">The categories assigned to the article.</param>
public sealed record ArticleSummary(
    Guid Id,
    string Name,
    string Slug,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<CategorySummary> Categories);
