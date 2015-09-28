using System;

namespace IsDebug.Core
{
    internal class RunnerSettings
    {
        public bool Recursive { get; set; }
        public string[] StartPaths { get; set; }

        public RunnerSettings()
        {
            this.Recursive = true;
            this.StartPaths = new string[] { Environment.CurrentDirectory };
        }
    }
}
