using QuickDeploy.CliOptions;

namespace QuickDeploy.Commands;

public interface IRunInit
{
    int Init(InitOptions opts);
}