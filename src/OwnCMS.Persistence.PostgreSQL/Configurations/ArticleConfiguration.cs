using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OwnCMS.Entities;

namespace OwnCMS.Persistence.PostgreSQL.Configurations;

internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("article");

        builder.HasKey(article => article.Id);

        builder.Property(article => article.Id)
            .HasColumnName("id");

        builder.Property(article => article.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(article => article.Slug)
            .HasColumnName("slug")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(article => article.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(article => article.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(article => article.Name)
            .IsUnique();

        builder.HasIndex(article => article.Slug)
            .IsUnique();

        builder.HasOne(article => article.ArticleContent)
            .WithOne(articleContent => articleContent.Article)
            .HasForeignKey<ArticleContent>(articleContent => articleContent.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(article => article.Categories)
            .WithMany(category => category.Articles)
            .UsingEntity<Dictionary<string, object>>(
                "article_category",
                right => right
                    .HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("category_id")
                    .HasConstraintName("category_id_fk")
                    .OnDelete(DeleteBehavior.Cascade),
                left => left
                    .HasOne<Article>()
                    .WithMany()
                    .HasForeignKey("article_id")
                    .HasConstraintName("article_id_fk")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.ToTable("article_category");
                    join.HasKey("article_id", "category_id");
                });
    }
}
