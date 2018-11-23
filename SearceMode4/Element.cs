using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            
            if (AssemblyDictionary == null)
                AssemblyDictionary = new Dictionary<string, string> {{EntryPath, SpecialPath}};
            else
                AssemblyDictionary.Add(EntryPath, SpecialPath);

            if(SourceDictionary == null)
                SourceDictionary = new Dictionary<string, List<Dictionary<string, string>>>();
            if (!SourceDictionary.ContainsKey(NameMod))
                SourceDictionary.Add(NameMod, new List<Dictionary<string, string>> {AssemblyDictionary});
            else
                SourceDictionary[NameMod].Add(AssemblyDictionary);
        }
        
        public string NameMod { get; set; }

        public string EntryPath { get; set; }

        public string SpecialPath { get; set; }

        internal Dictionary<string, string> AssemblyDictionary { get; set; }

        internal Dictionary<string, List <Dictionary<string, string>>> SourceDictionary { get; set; } 
        
        public override string ToString()
        {
            return $"NameMod {NameMod}\nEntryPath {EntryPath}\nSpecialPath {SpecialPath}\n";
        }
    }

    
}
