using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OwnCMS.Application.Articles;
using OwnCMS.Application.Categories;
using OwnCMS.Persistence.PostgreSQL.Articles;
using OwnCMS.Persistence.PostgreSQL.Categories;

namespace OwnCMS.Persistence.PostgreSQL.Extensions;

/// <summary>
/// Registers PostgreSQL persistence services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds PostgreSQL persistence services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">The PostgreSQL connection string.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddPostgreSqlPersistence(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        services.AddDbContext<OwnCmsDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IArticleDataAccess, PostgreSqlArticleDataAccess>();
        services.AddScoped<ICategoryDataAccess, PostgreSqlCategoryDataAccess>();

        return services;
    }
}
