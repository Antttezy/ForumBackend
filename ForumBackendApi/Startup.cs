using System.Text.Json.Serialization;

namespace ForumBackendApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

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