using OwnCMS.Application.Articles.Models;
using OwnCMS.Application.Categories;
using OwnCMS.Entities;

namespace OwnCMS.Application.Articles;

/// <summary>
/// Default application writer for article use cases.
/// </summary>
public sealed class ArticleWriter(
    IArticleDataAccess articleDataAccess,
    ICategoryDataAccess categoryDataAccess) : IArticleWriter
{
    public async Task<CreateArticleResult> CreateArticleAsync(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var errors = Validate(request);
        if (errors.Count > 0)
        {
            return CreateArticleResult.Failure(errors);
        }

        var articleSlug = SlugGenerator.Generate(request.Slug, request.Name);
        if (await articleDataAccess.ArticleSlugExistsAsync(articleSlug, cancellationToken))
        {
            return CreateArticleResult.Failure(["Article slug already exists."]);
        }

        var categories = await ResolveCategoriesAsync(request.Categories, cancellationToken);
        var assignedCategories = categories.ToList();
        var articleId = Guid.NewGuid();
        var article = new Article
        {
            Id = articleId,
            Name = request.Name.Trim(),
            Slug = articleSlug,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            Categories = assignedCategories
        };

        var articleContent = new ArticleContent
        {
            Id = Guid.NewGuid(),
            ArticleId = articleId,
            Html = request.Html,
            Css = request.Css,
            Article = article
        };

        article.ArticleContent = articleContent;

        foreach (var category in assignedCategories)
        {
            category.Articles.Add(article);
        }

        await articleDataAccess.CreateArticleAsync(article, cancellationToken);

        return CreateArticleResult.Success(articleId);
    }

    private async Task<IReadOnlyList<Category>> ResolveCategoriesAsync(
        IReadOnlyList<CreateArticleCategoryRequest> categoryRequests,
        CancellationToken cancellationToken)
    {
        var categories = new List<Category>();
        var processedSlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var categoryRequest in categoryRequests)
        {
            var normalizedName = categoryRequest.Name.Trim();
            var normalizedSlug = SlugGenerator.Generate(categoryRequest.Slug, normalizedName);

            if (!processedSlugs.Add(normalizedSlug))
            {
                continue;
            }

            var existingCategory = await categoryDataAccess.GetCategoryByNameOrSlugAsync(
                normalizedName,
                normalizedSlug,
                cancellationToken);

            if (existingCategory is not null)
            {
                categories.Add(existingCategory);
                continue;
            }

            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = normalizedName,
                Slug = normalizedSlug
            };

            var createdCategory = await categoryDataAccess.CreateCategoryAsync(newCategory, cancellationToken);
            categories.Add(createdCategory);
        }

        return categories;
    }

    private static List<string> Validate(CreateArticleRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors.Add("Article name is required.");
        }

        if (request.Categories is null)
        {
            errors.Add("Categories collection is required.");
            return errors;
        }

        foreach (var category in request.Categories)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                errors.Add("Category name is required.");
            }
        }

        var generatedArticleSlug = SlugGenerator.Generate(request.Slug, request.Name);
        if (string.IsNullOrWhiteSpace(generatedArticleSlug))
        {
            errors.Add("Article slug could not be generated.");
        }

        return errors;
    }
}
