using CommandLine;
using QuickDeploy.CliOptions;
using QuickDeploy.Commands;

namespace QuickDeploy;

public class Startup
{
    private readonly IRunSetup _runSetup;
    private readonly IRunInit _runInit;
    private readonly IRunDeploy _runDeploy;

    public Startup(IRunSetup runSetup, IRunInit runInit, IRunDeploy runDeploy)
    {
        _runSetup = runSetup;
        _runInit = runInit;
        _runDeploy = runDeploy;
    }

    public async void Run(string[] args)
    {
        await CommandLine.Parser.Default.ParseArguments<SetupOptions, InitOptions, DeployOptions>(args).MapResult(
            (SetupOptions opts) => _runSetup.SetupQuickDeploy(opts),
            (InitOptions opts) => _runInit.Init(opts),
            (DeployOptions opts) => _runDeploy.Deploy(opts),
            errs => Task.FromResult(0)
        );
    }
}