using IsDebug.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace IsDebug.Utils
{
    internal class AssemblyUtils
    {
        /// <summary>
        /// Load an assembly for a given path.
        /// </summary>
        /// <param name="assemblyPath">Path of the assembly to load</param>
        /// <param name="assembly">Loaded Assembly, null if failed</param>
        /// <returns>True is the assembly was successfully loaded, false otherwise.</returns>
        public static string Load(string assemblyPath, out Assembly assembly)
        {
            assembly = null;
            var error = string.Empty;
            if (string.IsNullOrEmpty(assemblyPath) || !File.Exists(assemblyPath))
            {
                return string.Empty;
            }

            try
            {
                assembly = Assembly.LoadFrom(assemblyPath);
            }
            catch(FileLoadException fex)
            {
                return string.Format("Could not load {0}. Reason : {1}", assemblyPath, fex);
            }
            catch (BadImageFormatException biex)
            {
                return string.Format("Could not load {0}. Reason :{1}", assemblyPath, biex);
            }

            return string.Empty;
        }

        /// <summary>
        /// see http://dave-black.blogspot.fr/2011/12/how-to-tell-if-assembly-is-debug-or.html
        /// </summary>
        /// <param name="assembly">Assembly to test</param>
        /// <returns>IsDebugResult instance.</returns>
        public static IsDebugResult IsDebug(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var result = new IsDebugResult();

            var attribs = assembly.GetCustomAttributes(typeof(DebuggableAttribute), false);

            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attribs.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean
                // it's a DEBUG build; we have to check the JIT Optimization flag
                // i.e. it could have the "generate PDB" checked but have JIT Optimization enabled
                var debuggableAttribute = attribs[0] as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    result.HasDebuggableAttribute = true;
                    result.IsJITOptimized = !debuggableAttribute.IsJITOptimizerDisabled;
                    // check for Debug Output "full" or "pdb-only"
                    result.DebugOutput = (debuggableAttribute.DebuggingFlags &
                                    DebuggableAttribute.DebuggingModes.Default) !=
                                    DebuggableAttribute.DebuggingModes.None
                                    ? DebugOutputType.Full : DebugOutputType.PdbOnly;
                }
            }
            else
            {
                result.IsJITOptimized = true;
            }

            return result;
        }
    }
}
