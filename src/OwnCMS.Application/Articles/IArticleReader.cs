using OwnCMS.Application.Articles.Models;

namespace OwnCMS.Application.Articles;

/// <summary>
/// Provides read operations for article-related application use cases.
/// </summary>
public interface IArticleReader
{
    /// <summary>
    /// Gets all articles available to the caller.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of article summaries.</returns>
    Task<IReadOnlyList<ArticleSummary>> GetArticlesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets an article by its slug.
    /// </summary>
    /// <param name="slug">The article slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The article details if found; otherwise, <see langword="null" />.</returns>
    Task<ArticleDetails?> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);

    /// <summary>
    /// Gets articles assigned to a category.
    /// </summary>
    /// <param name="categorySlug">The category slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of article summaries.</returns>
    Task<IReadOnlyList<ArticleSummary>> GetArticlesByCategorySlugAsync(string categorySlug, CancellationToken cancellationToken);
}
