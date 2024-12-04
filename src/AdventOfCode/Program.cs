using AdventOfCode.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        ConfigureServices(builder.Services);
        var host = builder.Build();
        await host.Services.GetRequiredService<AdventRunner>().Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var abstractType = typeof(Day);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => abstractType.IsAssignableFrom(p) && p is { IsInterface: false, IsAbstract: false });
        foreach (var type in types)
        {
            services.AddTransient(abstractType, type);
        }

        services.AddTransient<AdventRunner>();
    }
}