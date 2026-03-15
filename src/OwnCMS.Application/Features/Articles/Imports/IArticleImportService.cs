namespace OwnCMS.Application.Features.Articles.Imports;

public interface IArticleImportService
{
    Task<Guid> ImportMetadata(string articleName, string? articleSlug, string? category, CancellationToken cancellationToken);

    Task ImportContent(Guid articleId, Stream htmlStreamContent, Stream cssStreamContent, CancellationToken cancellationToken);
}