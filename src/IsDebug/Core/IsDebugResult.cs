namespace IsDebug.Core
{
    internal class IsDebugResult
    {
        public bool? HasDebuggableAttribute { get; set; }
        public bool? IsJITOptimized { get; set; }
        public BuildType Build { get { return this.IsJITOptimized??false ? BuildType.Release : BuildType.Debug; } }
        public DebugOutputType DebugOutput { get; set; }
    }
}
