using CommandLine;

namespace QuickDeploy.CliOptions;

[Verb(name: "deploy", HelpText = "Deploy your project to your remote server")]
public class DeployOptions
{
    [Option('u', "username", Required = false, HelpText = "override the setup username and provide a new one")]
    public string? username { get; set; }
    
    [Option('r', "remote", Required = false, HelpText = "override the setup remote server and provide a new one")]
    public string? server { get; set; }
    
    [Option('f', "local-folder", Required = false, HelpText = "provide the local folder you want to deploy, if empty the current folder will be deployed")]
    public string? localFolder { get; set; }
    
    [Option('s', "remote-folder", Required = false, HelpText = "provide the remote folder the files need to be uploaded to")]
    public string? remoteFolder { get; set; }
}