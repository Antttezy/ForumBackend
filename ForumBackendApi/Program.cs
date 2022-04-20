using ForumBackendApi;

IHost app = CreateHostBuilder(args).Build();
app.Run();

IHostBuilder CreateHostBuilder(string[] args) => 
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(c => 
            c.UseStartup<Startup>());