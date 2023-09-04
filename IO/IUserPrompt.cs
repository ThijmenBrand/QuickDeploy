namespace QuickDeploy.IO;

public interface IUserPrompt
{
    string Prompt(string message);
    string? PromptOptional(string message);
    void WriteText(string message, bool error);
}