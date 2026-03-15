namespace OwnCMS.Application.Features.Articles.DTOs;

public record ArticleDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string HtmlContent { get; init; }
    
    public string Css { get; init; }
    
    public string CreatedAt { get; init; }
    
    public string Category { get; init; }
}
