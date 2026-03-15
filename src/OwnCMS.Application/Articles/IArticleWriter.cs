using OwnCMS.Application.Articles.Models;

namespace OwnCMS.Application.Articles;

/// <summary>
/// Provides write operations for article-related application use cases.
/// </summary>
public interface IArticleWriter
{
    /// <summary>
    /// Creates an article with related content and category assignments.
    /// </summary>
    /// <param name="request">The article creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The article creation result.</returns>
    Task<CreateArticleResult> CreateArticleAsync(CreateArticleRequest request, CancellationToken cancellationToken);
}
