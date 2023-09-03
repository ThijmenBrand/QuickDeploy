using QuickDeploy.CliOptions;

namespace QuickDeploy.Commands;

public interface IRunSetup
{
    int SetupQuickDeploy(SetupOptions opts);
}