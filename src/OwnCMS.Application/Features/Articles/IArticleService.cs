using OwnCMS.Application.Features.Articles.DTOs;

namespace OwnCMS.Application.Features.Articles;

public interface IArticleService
{
    IEnumerable<ShortArticleDto> GetAll();
    
    IEnumerable<CategoryArticlesDto> GetArticlesGroupedByCategory();
    
    IEnumerable<ArticleLinkDto> GetUncategorizedArticles();
}