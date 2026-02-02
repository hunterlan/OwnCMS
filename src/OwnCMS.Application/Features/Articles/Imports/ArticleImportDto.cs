using System.ComponentModel.DataAnnotations;

namespace OwnCMS.Application.Features.Articles.Imports;

public record ArticleImportDto
{
    [Required]
    [Length(1, 255)]
    public string Name { get; init; }
    
    public string? Slug { get; init; }
    
    public string? Category { get; init; }
}