using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using Amelia.CEXPOC.Api;
using Amelia.CEXPOC.RunnerNamespace;

namespace Amelia.CEXPOC.DipendencyInjection
{
    public static class DependencyInjection
    {
        public static ServiceProvider Initialize()
        {
            var services = new ServiceCollection();
            LoggingSetup(services);

            services.AddSingleton<Runner>();

            return services.BuildServiceProvider();
        }

        private static void LoggingSetup(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/CEX_POC-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog(Log.Logger);
            });

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
}