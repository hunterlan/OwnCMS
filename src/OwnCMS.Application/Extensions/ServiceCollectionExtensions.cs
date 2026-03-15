using Microsoft.Extensions.DependencyInjection;
using OwnCMS.Application.Articles;
using OwnCMS.Application.Categories;

namespace OwnCMS.Application.Extensions;

/// <summary>
/// Registers application-layer services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds application-layer services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleReader, ArticleReader>();
        services.AddScoped<IArticleWriter, ArticleWriter>();
        services.AddScoped<ICategoryReader, CategoryReader>();

        return services;
    }
}
