namespace QuickDeploy.IO;

public class Config : IConfig
{
    public bool ConfigExists(string path) => File.Exists(path);
    
    public void WriteConfig(Dictionary<string, string> configOptions, string writePath)
    {
        using var sw = new StreamWriter(writePath, false);
        foreach (var option in configOptions)
        {
            sw.WriteLine(option.Key + ":" + option.Value);
        }
    }
}