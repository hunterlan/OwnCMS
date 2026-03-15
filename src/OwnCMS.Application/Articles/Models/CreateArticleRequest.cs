namespace OwnCMS.Application.Articles.Models;

/// <summary>
/// Represents the input required to create an article.
/// </summary>
/// <param name="Name">The article name.</param>
/// <param name="Slug">The optional article slug.</param>
/// <param name="Html">The article HTML.</param>
/// <param name="Css">The article CSS.</param>
/// <param name="Categories">The categories to assign to the article.</param>
public sealed record CreateArticleRequest(
    string Name,
    string? Slug,
    string Html,
    string Css,
    IReadOnlyList<CreateArticleCategoryRequest> Categories);
