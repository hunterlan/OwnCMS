using OwnCMS.Application.Extensions;
using OwnCMS.Persistence.PostgreSQL.Extensions;
using OwnCMS.Presentation.Extensions;

const string connectionStringKey = "OwnCMS";

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(connectionStringKey) ??
                       throw new InvalidOperationException("Connection string is not configured.");

builder.Services.AddApplicationServices();
builder.Services.AddPostgreSqlPersistence(connectionString);
builder.Services.AddPresentationServices();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
