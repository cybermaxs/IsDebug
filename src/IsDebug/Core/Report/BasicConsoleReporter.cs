using IsDebug.Utils;

namespace IsDebug.Core.Report
{
    internal class BasicConsoleReporter : IReporter
    {
        public virtual void Generate(RunnerResult runnerResult)
        {
            if (runnerResult == null) return;

            if (runnerResult.OverralSuccess)
            {
                ConsoleEx.Ok("Status {0}, Analyzed {1} assemblies", "OK", runnerResult.TotalFiles);
            }
            else
            {
                ConsoleEx.Error("Status {0}, Analyzed {1} assemblies (Debug : {2}, Release : {3}, Errors : {4})", "KO", runnerResult.TotalFiles, runnerResult.TotalDebug, runnerResult.TotalRelease, runnerResult.Errors.Count);
            }
        }
    }
}
