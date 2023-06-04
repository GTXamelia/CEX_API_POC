using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

public static class DependencyInjection
{
    public static ServiceProvider Initialize()
    {
        var services = new ServiceCollection();
        LoggingSetup(services);

        services.AddSingleton<Runner>();

        return services.BuildServiceProvider();
    }

    // Class here to setup logging
    private static void LoggingSetup(IServiceCollection services)
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/CEX_POC-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Add logging
        services.AddLogging(builder =>
        {
            builder.AddSerilog(Log.Logger);
        });

        // Registers all classes that inherit from the BaseApi class
        var baseApiType = typeof(BaseApi<>);
        var baseApiImplementations = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == baseApiType);
        foreach (var implementation in baseApiImplementations)
        {
            services.AddTransient(implementation);
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}