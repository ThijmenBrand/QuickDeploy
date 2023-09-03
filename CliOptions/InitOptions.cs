using CommandLine;

namespace QuickDeploy.CliOptions;

[Verb("init", HelpText = "Initialize a local QuickDeploy instance for a project")]
public class InitOptions
{
    [Option('f', "full", Required = false,
        HelpText = "Override the global configurations and define local username and server")]
    public bool Full { get; set; }
}