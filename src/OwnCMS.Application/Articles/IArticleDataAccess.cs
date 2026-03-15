using OwnCMS.Entities;

namespace OwnCMS.Application.Articles;

/// <summary>
/// Defines persistence-facing article data access required by the application layer.
/// </summary>
public interface IArticleDataAccess
{
    /// <summary>
    /// Gets all articles.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of articles.</returns>
    Task<IReadOnlyList<Article>> GetArticlesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets an article by slug.
    /// </summary>
    /// <param name="slug">The article slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The article if found; otherwise, <see langword="null" />.</returns>
    Task<Article?> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);

    /// <summary>
    /// Gets articles by category slug.
    /// </summary>
    /// <param name="categorySlug">The category slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of matching articles.</returns>
    Task<IReadOnlyList<Article>> GetArticlesByCategorySlugAsync(string categorySlug, CancellationToken cancellationToken);

    /// <summary>
    /// Determines whether an article slug is already in use.
    /// </summary>
    /// <param name="slug">The article slug.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see langword="true" /> when the slug exists; otherwise, <see langword="false" />.</returns>
    Task<bool> ArticleSlugExistsAsync(string slug, CancellationToken cancellationToken);

    /// <summary>
    /// Persists a new article.
    /// </summary>
    /// <param name="article">The article to persist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task CreateArticleAsync(Article article, CancellationToken cancellationToken);
}
