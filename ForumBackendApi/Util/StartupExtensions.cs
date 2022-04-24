using System.Security.Cryptography;
using ForumBackend.Core.Objects;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using ForumBackend.Mapping.Profiles;
using ForumBackendApi.Services;
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

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UserMappingProfile>();
            config.AddProfile<PostMappingProfile>();
        });
    }

    public static void AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    public static AccessTokenParameters GetAccessTokenParameters(IConfiguration configuration)
    {
        var secretKey = Convert.FromBase64String(configuration["AUTH_SECRET_KEY"]);

        RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(secretKey, out _);

        var lifetime = int.Parse(configuration["LIFETIME"]);

        return new()
        {
            Issuer = "FORUM-BACKEND",
            Audience = "FORUM-BACKEND",
            Lifetime = TimeSpan.FromMinutes(lifetime),
            SecurityKey = new(rsa)
        };
    }

    public static void AddAccessTokenParameters(this IServiceCollection services)
    {
        services.AddSingleton(factory =>
        {
            IConfiguration config = factory.GetRequiredService<IConfiguration>();
            return GetAccessTokenParameters(config);
        });
    }

    public static void AddAccessTokenTools(this IServiceCollection services)
    {
        services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();
        services.AddSingleton<IAccessTokenValidator, AccessTokenValidator>();
    }
    
    public static void AddAuthenticationService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public static void AddPostService(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
    }
}