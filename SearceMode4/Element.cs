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
        
        //private readonly Dictionary<string, Dictionary<string, string>> couple;

        internal Element(string nameMod, string relativePath, string specialPath)
        {
            NameMod = nameMod;
            RelativePath = relativePath;
            SpecialPath = specialPath;            
        }

        public string NameMod { get; set; }

        public string RelativePath { get; set; }

        public string SpecialPath { get; set; }

        public override string ToString()
        {
            return $"NameMod {NameMod}\nRelativePath {RelativePath}\nSpecialPath {SpecialPath}\n";
        }
    }
}
