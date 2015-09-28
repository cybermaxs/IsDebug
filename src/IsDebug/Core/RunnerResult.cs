using IsDebug.Core;
using System.Collections.Generic;
using System.Linq;

namespace IsDebug
{
    internal class RunnerResult
    {
        private Dictionary<string, IsDebugResult> results = new Dictionary<string, IsDebugResult>();
        private List<string> errors = new List<string>();      

        public RunnerResult()
        {

        }
        public void Ok(string assemblyPath, IsDebugResult result)
        {
            results[assemblyPath] = result;
        }

        public void Fail(string message)
        {
            errors.Add(message);
        }

        /// <summary>
        /// Return true if no errors and no debug assemblies.
        /// </summary>
        public bool OverralSuccess
        {
            get { return results.All(kvp => kvp.Value.Build == BuildType.Release) && errors.Count == 0; }
        }

        public int TotalFiles
        {
            get { return results.Count + errors.Count; }
        }

        public int TotalRelease
        {
            get { return results.Count(kvp => kvp.Value.Build == BuildType.Release); }
        }

        public int TotalDebug
        {
            get { return results.Count(kvp => kvp.Value.Build == BuildType.Debug); }
        }

        public IReadOnlyDictionary<string, IsDebugResult> ScanResults
        {
            get { return this.results; }
        }

        public IList<string> Errors
        {
            get { return this.errors; }
        }

    }
}
