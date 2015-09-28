using System.Collections.Generic;

namespace IsDebug.Core.Scan
{
    internal interface IScanner
    {
        IEnumerable<string> Scan(string startPath);
    }
}
