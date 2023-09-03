using QuickDeploy.CliOptions;

namespace QuickDeploy.Setup;

public interface IRunSetup
{
    int SetupQuickDeploy(SetupOptions opts);
}