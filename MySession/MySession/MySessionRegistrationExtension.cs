namespace MySession.MySession;

public static class MySessionRegistrationExtension
{
    public static IServiceCollection AddMySession(this IServiceCollection services)
    {
        services.AddSingleton<IMySessionStorage, MySessionStorage>();
        services.AddSingleton<ISessionStorageEngine>(services =>
        {
            var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
            Directory.CreateDirectory(path);
            return new FileSessionStorageEngine(path);
        });
        services.AddScoped<MySessionScopedContainer>();
        return services;
    }
    
}