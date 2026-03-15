using OwnCMS.Application.Contexts;
using OwnCMS.Entities;

namespace OwnCMS.Application.Features.Articles.Imports;

internal class ArticleImportService(OwnCmsContext cmsContext) : IArticleImportService
{
    public async Task<Guid> ImportMetadata(string articleName, string? articleSlug, string? category, CancellationToken cancellationToken)
    {
        articleSlug ??= articleName.ToLower().Replace(" ", "-");
        
        //TODO: Find category by ID or name or create new if name is provided.
        var content = new Content
        {
            CreatedAt = DateTime.UtcNow,
            Title = articleName,
            Slug = articleSlug,
            HtmlBody = [],
            Css = [],
        };
        
        cmsContext.Contents.Add(content);
        await cmsContext.SaveChangesAsync(cancellationToken);
        
        return content.Id;
    }

    public Task ImportContent(Guid articleId, Stream htmlStreamContent, Stream cssStreamContent, CancellationToken cancellationToken)
    {
        //TODO: Validate articleId exists.
        var article = cmsContext.Contents.Single(c => c.Id == articleId);
        
        article.HtmlBody = ReadStream(htmlStreamContent);
        article.Css = ReadStream(cssStreamContent);
        
        return cmsContext.SaveChangesAsync(cancellationToken);
    }

    private byte[] ReadStream(Stream stream)
    {
        List<byte> bytes = new();
        byte[] buffer = new byte[128];
        int read;

        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            bytes.AddRange(buffer.Take(read));
        }
        
        return bytes.ToArray();
    }
}