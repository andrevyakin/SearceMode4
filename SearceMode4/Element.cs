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
        
        public Element(string nameMod, string entryPath, string specialPath)
        {
            NameMod = nameMod;
            EntryPath = entryPath;
            SpecialPath = specialPath;
        }

        public string NameMod { get; set; }
        public string EntryPath { get; set; }
        public string SpecialPath { get; set; }
        
        public override string ToString()
        {
            return $"NameMod {NameMod}\nEntryPath {EntryPath}\nSpecialPath {SpecialPath}\n";
        }
    }
}
