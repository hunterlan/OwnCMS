using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OwnCMS.Persistence.PostgreSQL;

namespace OwnCMS.Migrations.PostgreSQL;

/// <summary>
/// Creates <see cref="OwnCmsDbContext"/> instances for EF Core design-time operations.
/// </summary>
public sealed class OwnCmsDbContextFactory : IDesignTimeDbContextFactory<OwnCmsDbContext>
{
    private const string ConnectionStringKey = "ConnectionStrings:OwnCMS";
    private const string ProjectFileName = "OwnCMS.Migrations.PostgreSQL.csproj";
    private const string SettingsFileName = "migrations.settings.json";
    private const string LocalSettingsFileName = "migrations.settings.local.json";

    /// <inheritdoc />
    public OwnCmsDbContext CreateDbContext(string[] args)
    {
        var projectDirectory = FindProjectDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile(SettingsFileName, optional: false)
            .AddJsonFile(LocalSettingsFileName, optional: true)
            .Build();
        var connectionString = configuration.GetConnectionString("OwnCMS");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Set '{ConnectionStringKey}' in '{SettingsFileName}' or '{LocalSettingsFileName}' before running EF Core commands.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<OwnCmsDbContext>();
        optionsBuilder.UseNpgsql(
            connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(OwnCmsDbContextFactory).Assembly.GetName().Name));

        return new OwnCmsDbContext(optionsBuilder.Options);
    }

    private static string FindProjectDirectory()
    {
        var currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);

        while (currentDirectory is not null)
        {
            var projectFilePath = Path.Combine(currentDirectory.FullName, ProjectFileName);

            if (File.Exists(projectFilePath))
            {
                return currentDirectory.FullName;
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new InvalidOperationException(
            $"Could not locate the '{ProjectFileName}' directory starting from '{AppContext.BaseDirectory}'.");
    }
}
