using QuickDeploy.CliOptions;
using QuickDeploy.IO;

namespace QuickDeploy.Commands;

public class RunSetup : IRunSetup
{
    private readonly IUserPrompt _userPrompt;
    private readonly IConfig _config;

    public RunSetup(IUserPrompt userPrompt, IConfig config)
    {
        _userPrompt = userPrompt;
        _config = config;
    }
    
    public int SetupQuickDeploy(SetupOptions opts)
    {
        if (_config.ConfigExists(IoConstants.GlobalConfigPath))
        {
            var overrideConfig = _userPrompt.Prompt("A config already exists, do you want to override it? Y / N: ");
            
            if (overrideConfig == "N" | overrideConfig == "n" | overrideConfig == "No" |
                overrideConfig == "NO") return 0;
        }
        
        Console.WriteLine("Welcome to the QuickDeploy setup tool");
        Console.WriteLine("--------------------------------------");

        var userInput = new Dictionary<string, string>();

        var username = _userPrompt.Prompt("Please enter your remote username: ");
        userInput.Add(IoConstants.UsernameConfigKey, username);

        var server = _userPrompt.Prompt("Please enter the address of your remote server: ");
        userInput.Add(IoConstants.ServerConfigKey, server);
        
        _config.WriteConfig(userInput, IoConstants.GlobalConfigPath);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Setup complete, QuickDeploy is ready to use");
        Console.ResetColor();

        return 0;
    }
}