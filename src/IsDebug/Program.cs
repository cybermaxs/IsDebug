using IsDebug.Core;
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

            if (showHelp || runnerSettings.StartPaths==null || runnerSettings.StartPaths.Length==0)
            {
                ShowHelp(options);
                return 0;
            }

            var runner = new Runner(runnerSettings);
            var runnerResult = runner.Run();

            if(runnerResult.OverralSuccess)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Status {0}, Analyzed {1} assemblies", "OK", runnerResult.TotalFiles);
                Console.ResetColor();
            }
                
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Status {0}, Analyzed {1} assemblies (Debug : {2}, Release : {3}, Errors : {4})", "KO", runnerResult.TotalFiles, runnerResult.TotalDebug, runnerResult.TotalRelease, runnerResult.Errors.Count);
                Console.ResetColor();
            }

            if (displayDetails)
            {
                //print results
                foreach (var r in runnerResult.ScanResults)
                {
                    Console.WriteLine("{0} (HasDebuggableAttribute :'{1}', IsJITOptimized : '{2}', DebugOutput : '{3}')", r.Key, r.Value.HasDebuggableAttribute, r.Value.IsJITOptimized, r.Value.DebugOutput);
                }

                Console.ForegroundColor = ConsoleColor.Red;
                foreach (var msg in runnerResult.Errors)
                    Console.WriteLine(msg);
                Console.ResetColor();
            }

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
