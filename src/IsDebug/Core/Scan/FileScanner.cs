using System.Collections.Generic;
using System.IO;

namespace IsDebug.Core.Scan
{
    /// <summary>
    /// a file-based scanner.
    /// </summary>
    internal class FileScanner : IScanner
    {
        private readonly bool recursive;

        public FileScanner(bool recursive)
        {
            this.recursive = recursive;
        }
        
        public IEnumerable<string> Scan(string startPath)
        {
            return Directory.EnumerateFiles(startPath, "*.dll", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}
