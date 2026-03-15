using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OwnCMS.Entities;

namespace OwnCMS.Persistence.PostgreSQL.Configurations;

internal sealed class ArticleContentConfiguration : IEntityTypeConfiguration<ArticleContent>
{
    public void Configure(EntityTypeBuilder<ArticleContent> builder)
    {
        builder.ToTable("article_content");

        builder.HasKey(articleContent => articleContent.Id);

        builder.Property(articleContent => articleContent.Id)
            .HasColumnName("id");

        builder.Property(articleContent => articleContent.ArticleId)
            .HasColumnName("article_id")
            .IsRequired();

        builder.Property(articleContent => articleContent.Html)
            .HasColumnName("html")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(articleContent => articleContent.Css)
            .HasColumnName("css")
            .HasColumnType("text")
            .IsRequired();

        builder.HasIndex(articleContent => articleContent.ArticleId)
            .IsUnique();
    }
}
