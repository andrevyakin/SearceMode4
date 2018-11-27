using System.Collections.Generic;

namespace SearceMode4
{
    internal class RootAssembly
    {
        private static RootAssembly instance;

        internal List<string> RootDirsAssembly { get; }

        internal List<string> RootFilesAssembly { get; }

        private RootAssembly(List<string> rootDirsAssembly = null, List<string> rootFilesAssembly = null)
        {
            RootDirsAssembly = rootDirsAssembly;
            RootFilesAssembly = rootFilesAssembly;
        }

        public static RootAssembly GetInstance(List<string> rootDirsAssembly = null, List<string> rootFilesAssembly = null)
        {
            return instance ?? (instance = new RootAssembly(rootDirsAssembly, rootFilesAssembly));
        }
    }
}