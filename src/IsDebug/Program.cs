using IsDebug.Core;
using IsDebug.Core.Report;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IsDebug
{
    class Program
    {
        static int Main(string[] args)
        {
            var runnerSettings = new RunnerSettings();
            var showHelp = false;
            var displayDetails = false;

            var options = new OptionSet
            {
                { "r|recursive", "Recursive", v => runnerSettings.Recursive=v!=null },
                { "h|help", "Show help", v => showHelp=true },
                { "d|details", "Display Details", v => displayDetails=true }
            };

            try
            {
                runnerSettings.StartPaths = options.Parse(args).ToArray();
            }
            catch (OptionException e)
            {
                Console.Write("invalid options: {0}", e.Message);
                Console.WriteLine("Try `isdebug --help' for more information.");
                return -1;
            }

            if (showHelp)
            {
                ShowHelp(options);
                return 0;
            }

            var runner = new Runner(runnerSettings);
            var runnerResult = runner.Run();

            var reporter = !displayDetails ? new BasicConsoleReporter() : new DetailedConsoleReporter();

            reporter.Generate(runnerResult);

            if (Debugger.IsAttached)
                Console.ReadKey();

            return runnerResult.OverralSuccess ? 0 : 1;
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: isdebug [OPTIONS] path");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
