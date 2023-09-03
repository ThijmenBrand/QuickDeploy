using CommandLine;
using QuickDeploy.CliOptions;
using QuickDeploy.Setup;

namespace QuickDeploy;

public class Startup
{
    [Verb("setup", HelpText = "Setup a QuickDeploy server for global use")]
    class SetupOptionsClass : SetupOptions {}

    private readonly IRunSetup _runSetup;

    public Startup(IRunSetup runSetup)
    {
        _runSetup = runSetup;
    }

    public int Run(string[] args)
    {
        return CommandLine.Parser.Default.ParseArguments<SetupOptionsClass>(args).MapResult(
            (SetupOptionsClass opts) => _runSetup.SetupQuickDeploy(opts), errs => 1
        );
    }
}