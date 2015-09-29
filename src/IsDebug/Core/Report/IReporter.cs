namespace IsDebug.Core.Report
{
    internal interface IReporter
    {
        void Generate(RunnerResult runnerResult);
    }
}
