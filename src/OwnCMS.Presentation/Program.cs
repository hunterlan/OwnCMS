using Microsoft.EntityFrameworkCore;
using OwnCMS.Application.Contexts;
using OwnCMS.Application.Extensions;
using OwnCMS.Presentation.Extensions;

const string connectionStringKey = "OwnCMS";

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString(connectionStringKey) ??
                throw new InvalidOperationException("Connection string is not found.");

builder.Services.AddDbContext<OwnCmsContext>(options => options.UseNpgsql(connString));
builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();