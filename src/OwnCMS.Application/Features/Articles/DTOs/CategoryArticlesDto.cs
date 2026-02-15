namespace OwnCMS.Application.Features.Articles.DTOs;

public record CategoryArticlesDto
{
    public Guid CategoryId { get; init; }
    
    public string CategoryName { get; init; }
    
    public List<ArticleLinkDto> Articles { get; init; }
}

public record ArticleLinkDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string Slug { get; init; }
}