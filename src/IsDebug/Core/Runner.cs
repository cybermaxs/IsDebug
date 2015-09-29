using IsDebug.Core.Scan;
using IsDebug.Utils;
using System;
using System.Reflection;

namespace IsDebug.Core
{
    internal class Runner
    {
        private readonly RunnerSettings settings;

        public Runner(RunnerSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            this.settings = settings;
        }

        public RunnerResult Run()
        {
            var scanner = new FileScanner(settings.Recursive);
            var results = new RunnerResult();

            foreach (var startPath in settings.StartPaths)
            {
                foreach (var path in scanner.Scan(startPath))
                {
                    Assembly assembly;
                    string errorMessage;
                    if ((errorMessage = AssemblyUtils.Load(path, out assembly)) == string.Empty)
                    {
                        IsDebugResult isdebugResult;
                        string debugerror = string.Empty;
                        if ((debugerror = AssemblyUtils.TryIsDebug(assembly, out isdebugResult)) == string.Empty)
                         results.Ok(path, isdebugResult);
                        else
                            results.Fail(errorMessage);
                    }
                    else
                        results.Fail(errorMessage);
                }
            }

            return results;
        }
    }
}
