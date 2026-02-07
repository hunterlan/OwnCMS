using System.Text;
using OwnCMS.Application.Contexts;
using OwnCMS.Application.Extractors;
using OwnCMS.Application.Features.Articles.DTOs;

namespace OwnCMS.Application.Features.Articles;

public class ArticleService(
    OwnCmsContext cmsContext,
    IHtmlContentExtractor htmlExtractor
    ) : IArticleService
{
    public IEnumerable<ShortArticleDto> GetAll()
    {
        var contents = cmsContext.Contents.AsQueryable();
        var categories = cmsContext.Categories.AsQueryable();
        
        var allRawArticles = from content in contents
            select new
            {
                Id = content.Id,
                Title = content.Title,
                HtmlContent = content.HtmlBody,
                Published = content.CreatedAt,
                Category = ""
            };

        var allArticles = allRawArticles.ToList().Select(ara => new ShortArticleDto
        {
            Id = ara.Id,
            Title = ara.Title,
            ShortContent = htmlExtractor.ExtractAndTruncate(Encoding.UTF8.GetString(ara.HtmlContent)),
            CreatedAt = ara.Published.ToString("yyyy-MM-dd HH:mm:ss"),
            Category = ara.Category
        });
        
        return allArticles;
    }
}