using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = new Assembly(@"E:\Games\Skyrim - Requiem for a Dream v3.6.2\Requiem for a Dream XP v3.6.2");

            assembly.Serialize("Assembly.xml");
            
            //Source source = new Source(@"E:\MySkyrimLE\Requiem for a Dream Source");

            //Matching matching = new Matching (assembly, source);
            
            //Copy elements = new Copy (null, assembly, source);

            //matching.Display();

            
        }
    }
}
