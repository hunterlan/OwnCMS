namespace OwnCMS.Application.Extractors;

public interface IHtmlContentExtractor
{
    string ExtractAndTruncate(string html);
}
