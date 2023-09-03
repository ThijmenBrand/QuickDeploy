using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using QuickDeploy.CliOptions;
using QuickDeploy.Setup;

namespace QuickDeploy;

class QuickDeploy
{


    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection();
        ConfigureServices(serviceProvider);
        serviceProvider.AddSingleton<Startup, Startup>().BuildServiceProvider().GetService<Startup>()?.Run(args);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRunSetup, RunSetup>();
    }
}