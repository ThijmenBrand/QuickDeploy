using CommandLine;
using QuickDeploy.CliOptions;
using QuickDeploy.Commands;

namespace QuickDeploy;

public class Startup
{
    private readonly IRunSetup _runSetup;
    private readonly IRunInit _runInit;

    public Startup(IRunSetup runSetup, IRunInit runInit)
    {
        _runSetup = runSetup;
        _runInit = runInit;
    }

    public void Run(string[] args)
    {
        CommandLine.Parser.Default.ParseArguments<SetupOptions, InitOptions>(args).MapResult(
            (SetupOptions opts) => _runSetup.SetupQuickDeploy(opts), (InitOptions opts) => _runInit.Init(opts), errs => 1
        );
    }
}