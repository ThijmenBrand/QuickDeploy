using QuickDeploy.CliOptions;

namespace QuickDeploy.Commands;

public interface IRunDeploy
{
    Task<int> Deploy(DeployOptions opts);
}