using OwnCMS.Application.Categories.Models;

namespace OwnCMS.Application.Articles.Models;

/// <summary>
/// Represents a full article view for detail scenarios.
/// </summary>
/// <param name="Id">The article identifier.</param>
/// <param name="Name">The article name.</param>
/// <param name="Slug">The article slug.</param>
/// <param name="Html">The article HTML.</param>
/// <param name="Css">The article CSS.</param>
/// <param name="CreatedAt">The article creation timestamp.</param>
/// <param name="UpdatedAt">The article update timestamp.</param>
/// <param name="Categories">The categories assigned to the article.</param>
public sealed record ArticleDetails(
    Guid Id,
    string Name,
    string Slug,
    string Html,
    string Css,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<CategorySummary> Categories);
