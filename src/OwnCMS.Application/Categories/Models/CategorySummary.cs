namespace OwnCMS.Application.Categories.Models;

/// <summary>
/// Represents a lightweight category view for read scenarios.
/// </summary>
/// <param name="Id">The category identifier.</param>
/// <param name="Name">The category name.</param>
/// <param name="Slug">The category slug.</param>
public sealed record CategorySummary(
    Guid Id,
    string Name,
    string Slug);
