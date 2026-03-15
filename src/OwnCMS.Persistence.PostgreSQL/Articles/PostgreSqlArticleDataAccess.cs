using Microsoft.EntityFrameworkCore;
using OwnCMS.Application.Articles;
using OwnCMS.Entities;

namespace OwnCMS.Persistence.PostgreSQL.Articles;

/// <summary>
/// PostgreSQL-backed article data access for the application layer.
/// </summary>
public sealed class PostgreSqlArticleDataAccess(OwnCmsDbContext dbContext) : IArticleDataAccess
{
    public async Task<IReadOnlyList<Article>> GetArticlesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Articles
            .AsNoTracking()
            .Include(article => article.ArticleContent)
            .Include(article => article.Categories)
            .OrderByDescending(article => article.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));

        return await dbContext.Articles
            .AsNoTracking()
            .Include(article => article.ArticleContent)
            .Include(article => article.Categories)
            .SingleOrDefaultAsync(article => article.Slug == slug, cancellationToken);
    }

    public async Task<IReadOnlyList<Article>> GetArticlesByCategorySlugAsync(string categorySlug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(categorySlug, nameof(categorySlug));

        return await dbContext.Articles
            .AsNoTracking()
            .Include(article => article.ArticleContent)
            .Include(article => article.Categories)
            .Where(article => article.Categories.Any(category => category.Slug == categorySlug))
            .OrderByDescending(article => article.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ArticleSlugExistsAsync(string slug, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));

        return dbContext.Articles.AnyAsync(article => article.Slug == slug, cancellationToken);
    }

    public async Task CreateArticleAsync(Article article, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(article);

        await dbContext.Articles.AddAsync(article, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
