using QuickDeploy.CliOptions;

namespace QuickDeploy.Commands;

public interface IRunSetup
{
    Task<int> SetupQuickDeploy(SetupOptions opts);
}