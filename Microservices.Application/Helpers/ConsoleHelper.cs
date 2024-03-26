namespace Microservices.Application.Helpers;
public static class ConsoleHelper
{
    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(FormattedDateTimeNow() + " - [ERROR] - ");
        Console.ResetColor();
        Console.Write(message);
        Console.WriteLine();
    }

    public static void ShowInformation(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(FormattedDateTimeNow() + " - [INFO] - ");
        Console.ResetColor();
        Console.Write(message);
        Console.WriteLine();
    }

    private static string FormattedDateTimeNow() => $"[{DateTime.Now.ToString("HH:mm:ss")}]";
}
