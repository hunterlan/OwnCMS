namespace OwnCMS.Entities;

/// <summary>
/// Represents an article in the shared CMS domain.
/// </summary>
public sealed class Article
{
    /// <summary>
    /// Gets or sets the article identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the article name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the article slug.
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Gets or sets when the article was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets when the article was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the article content.
    /// </summary>
    public ArticleContent ArticleContent { get; set; } = null!;

    /// <summary>
    /// Gets or sets the categories assigned to the article.
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
