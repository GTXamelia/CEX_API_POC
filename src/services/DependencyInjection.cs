using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static ServiceProvider Initialize()
    {
        var services = new ServiceCollection();
        services.AddSingleton<SuperCatagories>();
        services.AddSingleton<Runner>();
        return services.BuildServiceProvider();
    }
}