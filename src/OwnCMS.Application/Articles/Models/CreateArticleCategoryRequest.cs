namespace OwnCMS.Application.Articles.Models;

/// <summary>
/// Represents a requested category assignment during article creation.
/// </summary>
/// <param name="Name">The category name.</param>
/// <param name="Slug">The optional category slug.</param>
public sealed record CreateArticleCategoryRequest(
    string Name,
    string? Slug);
