using System.Text.RegularExpressions;

namespace OwnCMS.Application.Extractors;

public class HtmlContentExtractor : IHtmlContentExtractor
{
    private const int MaxLength = 250;

    public string ExtractAndTruncate(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        // Remove HTML tags using Regex
        string plainText = Regex.Replace(html, "<[^>]*>", string.Empty);

        // Decode HTML entities (optional but good practice)
        plainText = System.Net.WebUtility.HtmlDecode(plainText);

        // Replace multiple spaces/newlines with a single space
        plainText = Regex.Replace(plainText, @"\s+", " ").Trim();

        return plainText.Length <= MaxLength ? plainText : $"{plainText.Substring(0, MaxLength)}...";
    }
}
