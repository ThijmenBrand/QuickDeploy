namespace QuickDeploy.IO;

public interface IConfig
{
    void WriteConfig(Dictionary<string, string> configOptions, string writePath);
    bool ConfigExists(string path);
}