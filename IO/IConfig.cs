namespace QuickDeploy.IO;

public interface IConfig
{
    void WriteConfig(Dictionary<string, string> configOptions, string writePath);
    Dictionary<string, string> ReadConfig(string readPath);
    bool ConfigExists(string path);
    string LocalConfigPath();
}