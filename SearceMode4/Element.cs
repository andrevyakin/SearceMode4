using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    public class Element
    {
        public Element()
        { }
        
        internal Element(string nameMod, string entryPath, string specialPath)
        {
            //NameMod - Имя мода
            NameMod = nameMod;
            //EntryPath - Путь внутри мода
            EntryPath = entryPath;
            //SpecialPath - Путь от дирректории с модами до мода, для Sourse, или
            //Полный путь к файлу для Assembly
            SpecialPath = specialPath;            
        }

        public string NameMod { get; set; }

        public string EntryPath { get; set; }

        public string SpecialPath { get; set; }

        public override string ToString()
        {
            return $"NameMod {NameMod}\nRelativePath {EntryPath}\nSpecialPath {SpecialPath}\n";
        }
    }
}
