namespace OwnCMS.Presentation.Options;

public class AdminOptions
{
    public required string Key { get; init; }
    
    public bool IsEnabled { get; init; } = false;
}