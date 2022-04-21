using ForumBackend.Data.Context;
using ForumBackendApi;
using Microsoft.EntityFrameworkCore;

IHost app = CreateHostBuilder(args).Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    ForumContext context = scope.ServiceProvider.GetRequiredService<ForumContext>();

    if ((await context.Database.GetPendingMigrationsAsync()).Any())
    {
        await context.Database.MigrateAsync();
    }
}

app.Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(c =>
            c.UseStartup<Startup>());