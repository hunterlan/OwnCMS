using OwnCMS.Application.Articles.Models;
using OwnCMS.Application.Categories.Models;
using OwnCMS.Entities;

namespace OwnCMS.Application.Articles;

/// <summary>
/// Default application reader for article use cases.
/// </summary>
public sealed class ArticleReader(IArticleDataAccess articleDataAccess) : IArticleReader
{
    public async Task<IReadOnlyList<ArticleSummary>> GetArticlesAsync(CancellationToken cancellationToken)
    {
        var articles = await articleDataAccess.GetArticlesAsync(cancellationToken);

        return articles
            .Select(MapSummary)
            .ToList();
    }

    public async Task<ArticleDetails?> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);

        var article = await articleDataAccess.GetArticleBySlugAsync(slug, cancellationToken);

        return article is null ? null : MapDetails(article);
    }

    public async Task<IReadOnlyList<ArticleSummary>> GetArticlesByCategorySlugAsync(string categorySlug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(categorySlug);

        var articles = await articleDataAccess.GetArticlesByCategorySlugAsync(categorySlug, cancellationToken);

        return articles
            .Select(MapSummary)
            .ToList();
    }

    private static ArticleSummary MapSummary(Article article) =>
        new(
            article.Id,
            article.Name,
            article.Slug,
            article.CreatedAt,
            article.UpdatedAt,
            MapCategories(article.Categories)
            );

    private static ArticleDetails MapDetails(Article article) =>
        new(
            article.Id,
            article.Name,
            article.Slug,
            article.ArticleContent.Html,
            article.ArticleContent.Css,
            article.CreatedAt,
            article.UpdatedAt,
            MapCategories(article.Categories)
            );

    private static List<CategorySummary> MapCategories(IEnumerable<Category> categories)
    {
        return categories
            .Select(category => new CategorySummary(category.Id, category.Name, category.Slug))
            .OrderBy(category => category.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
