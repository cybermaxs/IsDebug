using IsDebug.Core;
using Xunit;

namespace IsDebug.Tests
{
    public class RunnerTests
    {
        [Fact]
        public void IsDebug()
        {
            var runner = new Runner(new RunnerSettings() { Recursive = true, StartPaths = new string[] {@"..\..\..\LibDebug\" } });
            var result =runner.Run();

            Assert.False(result.OverralSuccess);
        }

        [Fact]
        public void IsRelease()
        {
            var runner = new Runner(new RunnerSettings() { Recursive = true, StartPaths = new string[] { @"..\..\..\LibRelease\" } });
            var result = runner.Run();

            Assert.True(result.OverralSuccess);
        }
    }
}
