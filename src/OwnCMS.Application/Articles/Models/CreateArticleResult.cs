namespace OwnCMS.Application.Articles.Models;

/// <summary>
/// Represents the result of creating an article.
/// </summary>
public sealed record CreateArticleResult
{
    /// <summary>
    /// Gets the created article identifier when the operation succeeds.
    /// </summary>
    public Guid? ArticleId { get; init; }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool Succeeded { get; init; }

    /// <summary>
    /// Gets the operation errors when the operation fails.
    /// </summary>
    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Creates a success result.
    /// </summary>
    /// <param name="articleId">The created article identifier.</param>
    /// <returns>A success result.</returns>
    public static CreateArticleResult Success(Guid articleId)
    {
        return new CreateArticleResult
        {
            ArticleId = articleId,
            Succeeded = true
        };
    }

    /// <summary>
    /// Creates a failure result.
    /// </summary>
    /// <param name="errors">The operation errors.</param>
    /// <returns>A failure result.</returns>
    public static CreateArticleResult Failure(IReadOnlyList<string> errors)
    {
        return new CreateArticleResult
        {
            Errors = errors,
            Succeeded = false
        };
    }
}
