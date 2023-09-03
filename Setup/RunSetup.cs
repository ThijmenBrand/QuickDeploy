using QuickDeploy.CliOptions;

namespace QuickDeploy.Setup;

public class RunSetup : IRunSetup
{
    public int SetupQuickDeploy(SetupOptions opts)
    {
        Console.WriteLine("hello setup!");

        return 0;
    }
}