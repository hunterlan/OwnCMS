using FakeItEasy;
using OwnCMS.Application.Articles;
using OwnCMS.Application.Articles.Models;
using OwnCMS.Application.Categories;
using OwnCMS.Entities;

namespace OwnCMS.Application.UnitTests.Articles;

public sealed class ArticleWriterTests
{
    private readonly IArticleDataAccess articleDataAccess = A.Fake<IArticleDataAccess>();
    private readonly ICategoryDataAccess categoryDataAccess = A.Fake<ICategoryDataAccess>();
    private readonly ArticleWriter sut;

    public ArticleWriterTests()
    {
        sut = new ArticleWriter(articleDataAccess, categoryDataAccess);
    }

    [Fact]
    public async Task CreateArticleAsync_WhenRequestIsInvalid_ReturnsFailureWithoutPersisting()
    {
        var request = new CreateArticleRequest(
            " ",
            null,
            "<p>Hello</p>",
            string.Empty,
            [new CreateArticleCategoryRequest(" ", null)]);

        var result = await sut.CreateArticleAsync(request, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Null(result.ArticleId);
        Assert.Contains("Article name is required.", result.Errors);
        Assert.Contains("Category name is required.", result.Errors);
        Assert.Contains("Article slug could not be generated.", result.Errors);
        A.CallTo(() => articleDataAccess.ArticleSlugExistsAsync(A<string>._, A<CancellationToken>._))
            .MustNotHaveHappened();
        A.CallTo(() => articleDataAccess.CreateArticleAsync(A<Article>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task CreateArticleAsync_WhenArticleSlugAlreadyExists_ReturnsFailure()
    {
        var request = new CreateArticleRequest(
            "Existing slug article",
            null,
            "<p>Hello</p>",
            string.Empty,
            []);

        A.CallTo(() => articleDataAccess.ArticleSlugExistsAsync("existing-slug-article", A<CancellationToken>._))
            .Returns(true);

        var result = await sut.CreateArticleAsync(request, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Equal(["Article slug already exists."], result.Errors);
        A.CallTo(() => articleDataAccess.CreateArticleAsync(A<Article>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task CreateArticleAsync_WhenRequestIsValid_CreatesArticleAndOnlyMissingCategories()
    {
        var existingCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "News",
            Slug = "news"
        };
        Category? createdCategory = null;
        Article? persistedArticle = null;

        var request = new CreateArticleRequest(
            " Hello OwnCMS ",
            null,
            "<p>Hello</p>",
            "body { color: black; }",
            [
                new CreateArticleCategoryRequest(" News ", null),
                new CreateArticleCategoryRequest("Tips", null),
                new CreateArticleCategoryRequest(" tips ", null)
            ]);

        A.CallTo(() => articleDataAccess.ArticleSlugExistsAsync("hello-owncms", A<CancellationToken>._))
            .Returns(false);

        A.CallTo(() => categoryDataAccess.GetCategoryByNameOrSlugAsync("News", "news", A<CancellationToken>._))
            .Returns(existingCategory);
        A.CallTo(() => categoryDataAccess.GetCategoryByNameOrSlugAsync("Tips", "tips", A<CancellationToken>._))
            .Returns((Category?)null);
        A.CallTo(() => categoryDataAccess.CreateCategoryAsync(A<Category>._, A<CancellationToken>._))
            .Invokes(call => createdCategory = call.GetArgument<Category>(0))
            .ReturnsLazily((Category category, CancellationToken _) => Task.FromResult(category));
        A.CallTo(() => articleDataAccess.CreateArticleAsync(A<Article>._, A<CancellationToken>._))
            .Invokes(call => persistedArticle = call.GetArgument<Article>(0))
            .Returns(Task.CompletedTask);

        var result = await sut.CreateArticleAsync(request, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.ArticleId);
        Assert.NotNull(persistedArticle);
        Assert.Equal(result.ArticleId, persistedArticle.Id);
        Assert.Equal("Hello OwnCMS", persistedArticle.Name);
        Assert.Equal("hello-owncms", persistedArticle.Slug);
        Assert.Null(persistedArticle.UpdatedAt);
        Assert.NotEqual(default, persistedArticle.CreatedAt);
        Assert.NotNull(persistedArticle.ArticleContent);
        Assert.Equal(persistedArticle.Id, persistedArticle.ArticleContent.ArticleId);
        Assert.Equal("<p>Hello</p>", persistedArticle.ArticleContent.Html);
        Assert.Equal("body { color: black; }", persistedArticle.ArticleContent.Css);
        Assert.Collection(
            persistedArticle.Categories,
            category => Assert.Same(existingCategory, category),
            category => Assert.Equal("tips", category.Slug));
        Assert.Contains(persistedArticle, existingCategory.Articles);
        Assert.NotNull(createdCategory);
        Assert.Contains(persistedArticle, createdCategory.Articles);

        A.CallTo(() => categoryDataAccess.GetCategoryByNameOrSlugAsync("Tips", "tips", A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => categoryDataAccess.CreateCategoryAsync(A<Category>.That.Matches(category =>
                category.Name == "Tips" &&
                category.Slug == "tips"),
            A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}
