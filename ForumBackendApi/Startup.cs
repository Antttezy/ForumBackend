using System.Text.Json.Serialization;
using ForumBackend.Core.Objects;
using ForumBackendApi.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ForumBackendApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAccessTokenParameters();
        services.AddAccessTokenTools();
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                AccessTokenParameters parameters = StartupExtensions.GetAccessTokenParameters(_configuration);

                options.TokenValidationParameters = new()
                {
                    ValidIssuer = parameters.Issuer,
                    ValidAudience = parameters.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = parameters.SecurityKey
                };
            });
        
        services.AddAutoMapper();
        services.AddPostgresDatabase(_configuration);
        services.AddUserService();
        services.AddAuthenticationService();
        services.AddPostService();
        services.AddCommentService();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoint => endpoint.MapDefaultControllerRoute());
    }
}