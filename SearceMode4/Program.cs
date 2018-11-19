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
            Source sourceWithPath = new Source(@"E:\MySkyrimLE\Mods");
            sourceWithPath.Serialize(@"E:\Projekts VS\SearceMode4\SearceMode4\SourceWithPath.xml");
            Source sourceWithoutPath = new Source(@"E:\MySkyrimLE\Requiem for a Dream Source");
            sourceWithoutPath.Serialize(@"E:\Projekts VS\SearceMode4\SearceMode4\SourceWithoutPath.xml");
            Console.ReadKey();
        }
    }
}
