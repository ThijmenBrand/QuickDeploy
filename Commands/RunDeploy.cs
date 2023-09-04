using System.Diagnostics;
using QuickDeploy.CliOptions;
using QuickDeploy.IO;
using QuickDeploy.Models;
using Renci.SshNet;

namespace QuickDeploy.Commands;

public class RunDeploy : IRunDeploy
{
    private readonly IConfig _config;
    private readonly IUserPrompt _userPrompt;
    
    public RunDeploy(IConfig config, IUserPrompt userPrompt)
    {
        _config = config;
        _userPrompt = userPrompt;
    }
    
    public async Task<int> Deploy(DeployOptions opts)
    {
        var config = await GetConfig(opts);

        if (string.IsNullOrEmpty(config.username) || string.IsNullOrEmpty(config.server))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No username or server provided!");
            Console.ResetColor();

            return 1;
        }

        if (string.IsNullOrEmpty(config.localFolder)) config.localFolder = Directory.GetCurrentDirectory();

        if (!Directory.Exists(config.localFolder))
        {
            _userPrompt.WriteText("Provided directory does not exist!", true);
            return 0;
        }

        var command = $@"/c scp -r {config.localFolder}/** {config.username}@{config.server}:{config.remoteFolder}";
        var process = new System.Diagnostics.Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = command,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        process.StartInfo = startInfo;
        process.Start();

        var error = process.StandardError.ReadToEnd();
        if(!string.IsNullOrEmpty(error)) { 
            _userPrompt.WriteText($"error occured: {error}", true);
            return 0;
        } 
        
        process.WaitForExit();
        
        _userPrompt.WriteText($"contents of target folder: {config.localFolder} are successfully uploaded", false);
        
        return 0;
    }

    private async Task<ConfigurationOptions> GetConfig(DeployOptions opts)
    {
        var server = opts.server;
        var username = opts.username;
        var localFolder = opts.localFolder;
        var remoteFolder = opts.remoteFolder;

        if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username))
        {
            var localConfig = _config.ReadConfig(_config.LocalConfigPath());
            
            if (string.IsNullOrEmpty(server)) server = localConfig[IoConstants.ServerConfigKey];
            if (string.IsNullOrEmpty(username)) username = localConfig[IoConstants.UsernameConfigKey];
            if (string.IsNullOrEmpty(localFolder)) localFolder = localConfig[IoConstants.LocalDeployFolderConfigKey];
            if (string.IsNullOrEmpty(remoteFolder)) remoteFolder = localConfig[IoConstants.RemoteDeployFolderConfigKey];
        }

        if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username))
        {
            var globalConfig = _config.ReadConfig(IoConstants.GlobalConfigPath);
            
            if (string.IsNullOrEmpty(server)) server = globalConfig[IoConstants.ServerConfigKey];
            if (string.IsNullOrEmpty(username)) username = globalConfig[IoConstants.UsernameConfigKey];
        }

        var options = new ConfigurationOptions
        {
            server = server,
            username = username,
            localFolder = localFolder,
            remoteFolder = remoteFolder,
        };

        return options;
    }
}