using Microsoft.Extensions.DependencyInjection;
using OwnCMS.Application.Extractors;
using OwnCMS.Application.Features.Articles;
using OwnCMS.Application.Features.Articles.Imports;

namespace OwnCMS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleImportService, ArticleImportService>();
        services.AddScoped<IHtmlContentExtractor, HtmlContentExtractor>();
        services.AddScoped<IArticleService, ArticleService>();
        
        return services;
    }
}
