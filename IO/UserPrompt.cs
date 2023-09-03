namespace QuickDeploy.IO;

public class UserPrompt : IUserPrompt
{
    public string Prompt(string message)
    {
        Console.Write(message);
        string? input;
        
        do
        {
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input can not be empty");
                Console.ResetColor();
            }
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    public string? PromptOptional(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}