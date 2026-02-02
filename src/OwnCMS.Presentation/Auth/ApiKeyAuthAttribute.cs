using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OwnCMS.Presentation.Options;

namespace OwnCMS.Presentation.Auth;

[AttributeUsage(AttributeTargets.Class)]
public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string HeaderName = "X-Admin-Key";
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var apiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        var adminOptions = context.HttpContext.RequestServices.GetRequiredService<AdminOptions>();

        if (!adminOptions.IsEnabled || string.IsNullOrWhiteSpace(apiKey)
                                    || !string.Equals(adminOptions.Key, apiKey, StringComparison.InvariantCulture))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}