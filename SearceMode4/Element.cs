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
            //couple = new Dictionary<string, Dictionary<string, string>>();
            //couple[nameMod].Add(relativePath, specialPath);
        }

        internal string NameMod { get; }

        internal string RelativePath { get; }

        internal string SpecialPath { get; }

        public override string ToString()
        {
            return $"NameMod {NameMod}\nRelativePath {RelativePath}\nSpecialPath {SpecialPath}\n";
        }
    }
}
