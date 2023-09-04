namespace QuickDeploy.Models;

public class ConfigurationOptions
{
    public string server { get; set; }
    public string username { get; set; }
    public string? localFolder { get; set; }
    public string? remoteFolder { get; set; }
}