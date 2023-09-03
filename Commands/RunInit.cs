using QuickDeploy.CliOptions;
using QuickDeploy.IO;

namespace QuickDeploy.Commands;

public class RunInit : IRunInit
{
    private readonly IConfig _config;
    private readonly IUserPrompt _prompt;

    public RunInit(IConfig config, IUserPrompt prompt)
    {
        _config = config;
        _prompt = prompt;
    }
    public int Init(InitOptions opts)
    {
        var initOptions = new Dictionary<string, string>();
        
        if (opts.Full)
        {
            var username = _prompt.Prompt("Please enter your remote username: ");
            initOptions.Add(IoConstants.UsernameConfigKey, username);

            var server = _prompt.Prompt("Please enter the address of your remote server: ");
            initOptions.Add(IoConstants.ServerConfigKey, server);
        }
        
        var localDeployFolder = _prompt.Prompt("Please enter you local deploy folder (optional): ");
        initOptions.Add(IoConstants.LocalDeployFolderConfigKey, localDeployFolder);

        var removeDeployFolder = _prompt.Prompt("Please enter your remote folder path (optional): ");
        initOptions.Add(IoConstants.RemoteDeployFolderConfigKey, removeDeployFolder);

        var localPath = Directory.GetCurrentDirectory() + @"\.QuickDeploy";
        _config.WriteConfig(initOptions, localPath);

        return 0;
    }
}