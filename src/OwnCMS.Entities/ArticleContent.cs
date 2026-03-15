namespace OwnCMS.Entities;

/// <summary>
/// Represents the stored content body for an article.
/// </summary>
public sealed class ArticleContent
{
    /// <summary>
    /// Gets or sets the article content identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the related article identifier.
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// Gets or sets the article HTML.
    /// </summary>
    public string Html { get; set; } = null!;

    /// <summary>
    /// Gets or sets the article CSS.
    /// </summary>
    public string Css { get; set; } = null!;

    /// <summary>
    /// Gets or sets the related article.
    /// </summary>
    public Article Article { get; set; } = null!;
}
