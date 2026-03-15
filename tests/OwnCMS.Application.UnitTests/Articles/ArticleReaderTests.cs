using FakeItEasy;
using OwnCMS.Application.Articles;
using OwnCMS.Entities;

namespace OwnCMS.Application.UnitTests.Articles;

public sealed class ArticleReaderTests
{
    private readonly IArticleDataAccess articleDataAccess = A.Fake<IArticleDataAccess>();
    private readonly ArticleReader sut;

    public ArticleReaderTests()
    {
        sut = new ArticleReader(articleDataAccess);
    }

    [Fact]
    public async Task GetArticleBySlugAsync_WhenArticleExists_ReturnsMappedDetails()
    {
        var articleId = Guid.NewGuid();
        var createdAt = new DateTime(2026, 3, 15, 12, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2026, 3, 15, 13, 0, 0, DateTimeKind.Utc);
        var article = new Article
        {
            Id = articleId,
            Name = "Test article",
            Slug = "test-article",
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
            ArticleContent = new ArticleContent
            {
                Id = Guid.NewGuid(),
                ArticleId = articleId,
                Html = "<p>Hello</p>",
                Css = "body { color: red; }"
            },
            Categories =
            [
                new Category { Id = Guid.NewGuid(), Name = "zeta", Slug = "zeta" },
                new Category { Id = Guid.NewGuid(), Name = "Alpha", Slug = "alpha" }
            ]
        };

        A.CallTo(() => articleDataAccess.GetArticleBySlugAsync("test-article", A<CancellationToken>._))
            .Returns(article);

        var result = await sut.GetArticleBySlugAsync("test-article", CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(articleId, result.Id);
        Assert.Equal("Test article", result.Name);
        Assert.Equal("test-article", result.Slug);
        Assert.Equal("<p>Hello</p>", result.Html);
        Assert.Equal("body { color: red; }", result.Css);
        Assert.Equal(createdAt, result.CreatedAt);
        Assert.Equal(updatedAt, result.UpdatedAt);
        Assert.Collection(
            result.Categories,
            category => Assert.Equal("Alpha", category.Name),
            category => Assert.Equal("zeta", category.Name));
    }

    [Fact]
    public async Task GetArticlesAsync_ReturnsMappedSummaries()
    {
        var article = new Article
        {
            Id = Guid.NewGuid(),
            Name = "Summary article",
            Slug = "summary-article",
            CreatedAt = new DateTime(2026, 3, 15, 14, 0, 0, DateTimeKind.Utc),
            ArticleContent = new ArticleContent
            {
                Id = Guid.NewGuid(),
                ArticleId = Guid.NewGuid(),
                Html = "<p>Unused</p>",
                Css = string.Empty
            },
            Categories =
            [
                new Category { Id = Guid.NewGuid(), Name = "Guides", Slug = "guides" }
            ]
        };

        A.CallTo(() => articleDataAccess.GetArticlesAsync(A<CancellationToken>._))
            .Returns([article]);

        var result = await sut.GetArticlesAsync(CancellationToken.None);

        var summary = Assert.Single(result);
        Assert.Equal(article.Id, summary.Id);
        Assert.Equal("Summary article", summary.Name);
        Assert.Equal("summary-article", summary.Slug);
        Assert.Single(summary.Categories);
        Assert.Equal("Guides", summary.Categories[0].Name);
    }

    [Fact]
    public async Task GetArticlesByCategorySlugAsync_WhenCategorySlugIsBlank_ThrowsArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetArticlesByCategorySlugAsync(" ", CancellationToken.None));
    }
}
