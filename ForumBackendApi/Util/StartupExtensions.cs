using ForumBackend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Util;

public static class StartupExtensions
{
    public static void AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionTemplate = configuration.GetConnectionString("Postgres");
        var userId = configuration["POSTGRES_USERNAME"];
        var password = configuration["POSTGRES_PASSWORD"];
        var databaseName = configuration["POSTGRES_DATABASE_NAME"];
        var host = configuration["POSTGRES_HOST"];
        var port = configuration["POSTGRES_PORT"];

        var connection = string.Format(connectionTemplate, userId, password, host, port, databaseName);

        services.AddDbContext<ForumContext>(options =>
        {
            options.UseNpgsql(connection, b => b.MigrationsAssembly("ForumBackendApi"));
        });
    }
}