namespace QuickDeploy.IO;

public class Config : IConfig
{
    public bool ConfigExists(string path) => File.Exists(path);
    public string LocalConfigPath() => Directory.GetCurrentDirectory() + @"\" + IoConstants.ConfigFileName;

    
    public void WriteConfig(Dictionary<string, string> configOptions, string writePath)
    {
        using var sw = new StreamWriter(writePath, false);
        foreach (var option in configOptions)
        {
            sw.WriteLine(option.Key + ":" + option.Value);
        }
    }

    public Dictionary<string, string> ReadConfig(string readPath)
    {
        var lines = File.ReadAllLines(readPath);

        var outputDictionary = lines.Select(line => line.Split(":")).Where(keyValuePair => keyValuePair.Any() && keyValuePair.Length == 2).ToDictionary(keyValuePair => keyValuePair[0], keyValuePair => keyValuePair[1]);

        return outputDictionary;
    }
}