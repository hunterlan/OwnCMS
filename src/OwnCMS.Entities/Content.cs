namespace OwnCMS.Entities;

public partial class Content
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public byte[] HtmlBody { get; set; } = null!;

    public byte[] Css { get; set; } = null!;

    public Guid? CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }
}
