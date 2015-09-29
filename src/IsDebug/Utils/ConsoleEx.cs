using System;

namespace IsDebug.Utils
{
    internal static class ConsoleEx
    {
        public static void WriteLine(ConsoleColor color, string message, params object[] parameters)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message, parameters);
            Console.ResetColor();
        }

        public static void Info(string message, params object[] parameters)
        {
            WriteLine(ConsoleColor.DarkGray, message, parameters);
        }

        public static void Ok(string message, params object[] parameters)
        {
            WriteLine(ConsoleColor.DarkGreen, message, parameters);
        }

        public static void Warning(string message, params object[] parameters)
        {
            WriteLine(ConsoleColor.DarkYellow, message, parameters);
        }

        public static void Error(string message, params object[] parameters)
        {
            WriteLine(ConsoleColor.DarkRed, message, parameters);
        }
    }
}
