namespace OwnCMS.Entities;

/// <summary>
/// Represents a category that can group multiple articles.
/// </summary>
public sealed class Category
{
    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the category slug.
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Gets or sets the articles assigned to the category.
    /// </summary>
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
