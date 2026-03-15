using System.Text;

namespace OwnCMS.Application.Articles;

internal static class SlugGenerator
{
    public static string Generate(string? explicitSlug, string? fallbackText)
    {
        var input = string.IsNullOrWhiteSpace(explicitSlug) ? fallbackText : explicitSlug;
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var trimmed = input.Trim().ToLowerInvariant();
        var builder = new StringBuilder(trimmed.Length);
        var previousWasSeparator = false;

        foreach (var character in trimmed)
        {
            if (char.IsLetterOrDigit(character))
            {
                builder.Append(character);
                previousWasSeparator = false;
                continue;
            }

            if (previousWasSeparator)
            {
                continue;
            }

            builder.Append('-');
            previousWasSeparator = true;
        }

        return builder.ToString().Trim('-');
    }
}
