using System;

namespace IsDebug.Utils
{
    internal static class Logger
    {
        public static void Info(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
        }

        public static void Warning(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
        }

        public static void Error(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
        }

        private static void Format(string message, object[] parameters)
        {

        }

    }
}
