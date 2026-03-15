using Microsoft.EntityFrameworkCore;
using OwnCMS.Entities;
using OwnCMS.Persistence.PostgreSQL.Configurations;

namespace OwnCMS.Persistence.PostgreSQL;

/// <summary>
/// Entity Framework Core database context for OwnCMS PostgreSQL persistence.
/// </summary>
public sealed class OwnCmsDbContext(DbContextOptions<OwnCmsDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets the articles set.
    /// </summary>
    public DbSet<Article> Articles => Set<Article>();

    /// <summary>
    /// Gets the article contents set.
    /// </summary>
    public DbSet<ArticleContent> ArticleContents => Set<ArticleContent>();

    /// <summary>
    /// Gets the categories set.
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleContentConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
}
