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
            #region Дома
            ////Assembly assembly = new Assembly(@"E:\Games\Skyrim - Requiem for a Dream v3.6.2\Requiem for a Dream XP v3.6.2");
            //Assembly assembly = new Assembly(@"E:\Projekts VS\SearceMode4\SearceMode4\Assembly.xml");
            
            ////Source source = new Source(@"E:\MySkyrimLE\Mods");
            ////source.Serialize(@"E:\Projekts VS\SearceMode4\SearceMode4\Source.xml");

            //Source source = new Source(@"E:\Projekts VS\SearceMode4\SearceMode4\Source.xml");

            //source.Add(@"E:\MySkyrimLE\Requiem for a Dream Source");
            //source.Serialize(@"E:\Projekts VS\SearceMode4\SearceMode4\Source.xml");
            #endregion

            #region На работе
            Assembly assembly = new Assembly(@"D:\Skyrim LE\Skyrim - Requiem for a Dream v3.6.2\Requiem for a Dream XP v3.6.2");
            //assembly.Serialize(@"D:\source\repos\SearceMode4\SearceMode4\Assembly.xml");

            //Assembly assembly = new Assembly(@"D:\source\repos\SearceMode4\SearceMode4\Assembly.xml");
            Source source = new Source(@"D:\source\repos\SearceMode4\SearceMode4\Source.xml");
            //Matching matching = new Matching(assembly, source);
            //Console.ReadKey();
            //Repacking.RepackingRar(@"D:\Skyrim LE\Mods\Weapons\Tamrielic Lore - Chrysamere\tamrielic_lore v.4.rar");

            #endregion
            //assembly.Display();
            //source.Display();
            Matching matching = new Matching(assembly, source);

            matching.Display(true);
        }
    }
}
