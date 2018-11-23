using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    internal class Copy : Matching
    {
        private readonly string pathResult;

        internal Copy(string pathResult, Assembly assembly, Source source) : base(assembly, source)
        {
            this.pathResult = pathResult;
            Create();
        }

        internal Copy(string pathResult, string pathAssembly, string pathSource) : base(pathAssembly, pathSource)
        {
            this.pathResult = pathResult;
            Create();
        }

        private new void Create()
        {
            base.Create();

            if (Core == null)
            {
                Messenger.Message("Что-то пошло не так...");
                return;
            }

            foreach (var dir in Core)
            {
                foreach (var file in dir.NameMod)
                {
                    
                }
            }
        }
    }
}
