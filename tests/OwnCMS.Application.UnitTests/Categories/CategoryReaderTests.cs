using FakeItEasy;
using OwnCMS.Application.Categories;
using OwnCMS.Entities;

namespace OwnCMS.Application.UnitTests.Categories;

public sealed class CategoryReaderTests
{
    private readonly ICategoryDataAccess categoryDataAccess = A.Fake<ICategoryDataAccess>();
    private readonly CategoryReader sut;

    public CategoryReaderTests()
    {
        sut = new CategoryReader(categoryDataAccess);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnsAlphabeticallySortedSummaries()
    {
        A.CallTo(() => categoryDataAccess.GetCategoriesAsync(A<CancellationToken>._))
            .Returns(
            [
                new Category { Id = Guid.NewGuid(), Name = "zeta", Slug = "zeta" },
                new Category { Id = Guid.NewGuid(), Name = "Alpha", Slug = "alpha" }
            ]);

        var result = await sut.GetCategoriesAsync(CancellationToken.None);

        Assert.Collection(
            result,
            category => Assert.Equal("Alpha", category.Name),
            category => Assert.Equal("zeta", category.Name));
    }
}
