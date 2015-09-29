using IsDebug.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsDebug.Core.Report
{
    internal class DetailedConsoleReporter : BasicConsoleReporter
    {
        public override void Generate(RunnerResult runnerResult)
        {
            base.Generate(runnerResult);

            if (runnerResult.ScanResults != null)
                foreach (var r in runnerResult.ScanResults)
                {
                    if (r.Value.Build == BuildType.Release)
                        ConsoleEx.Info("{0} (HasDebuggableAttribute :'{1}', IsJITOptimized : '{2}', DebugOutput : '{3}')", r.Key, r.Value.HasDebuggableAttribute, r.Value.IsJITOptimized, r.Value.DebugOutput);
                    else
                        ConsoleEx.Warning("{0} (HasDebuggableAttribute :'{1}', IsJITOptimized : '{2}', DebugOutput : '{3}')", r.Key, r.Value.HasDebuggableAttribute, r.Value.IsJITOptimized, r.Value.DebugOutput);
                }

            //errors
            foreach (var msg in runnerResult.Errors)
                ConsoleEx.Error(msg);
        }

    }
}
