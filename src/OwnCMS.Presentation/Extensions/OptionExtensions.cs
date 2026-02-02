using OwnCMS.Presentation.Options;

namespace OwnCMS.Presentation.Extensions;

public static class OptionExtensions
{
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureAdminOptions(services, configuration);
        
        return services;
    }
    
    private static void ConfigureAdminOptions(IServiceCollection services, IConfiguration configuration)
    {
        var adminOptions = configuration.GetRequiredSection("AdminConfiguration").Get<AdminOptions>()!;
        
        services.AddSingleton(adminOptions);
    }
}