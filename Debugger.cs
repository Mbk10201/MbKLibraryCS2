using Serilog;

namespace MBKLibrary;

public class Debugger
{
    public static void Log(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("[MBKLibrary-Debugger] " + message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
