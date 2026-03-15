namespace OwnCMS.Application.Features.Articles.DTOs;

public record ShortArticleDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string ShortContent { get; init; }
    
    public string CreatedAt { get; init; }
    
    public string Category { get; init; }
    
    public string Slug { get; init; }
}