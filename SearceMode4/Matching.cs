using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    internal class Matching : Collection
    {

        private readonly Assembly assembly;
        private readonly Source source;

        protected Matching()
        { }

        internal Matching(Assembly assembly, Source source)
        {
            this.assembly = assembly;
            this.source = source;
           // if (Result == null)
                Create();
        }

        internal Matching(string pathAssembly, string pathSource)
        {
            assembly = new Assembly(pathAssembly);
            source = new Source(pathSource);
           // if (Result == null)
                Create();
        }
        
        //protected Dictionary<string, Dictionary<string, string>> Result { get; private set; }              

        private int countFile;

        protected override void Create()
        {
            Core = new List<Element>();
            countFile = 0;
            foreach (var assemblyItem in assembly)
            {
                var flagNotFound = false;
                foreach (var sourceItem in source)
                {
                    if (String.Compare(assemblyItem.EntryPath, sourceItem.EntryPath,
                            StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Core.Add(new Element(sourceItem.NameMod, assemblyItem.EntryPath, assemblyItem.SpecialPath));
                        flagNotFound = true;
                        countFile++;
                        break;
                    }
                }

                if (!flagNotFound)
                {
                    Core.Add(new Element("Not found", assemblyItem.EntryPath, assemblyItem.SpecialPath));

                }
            }
        }

        public new void Display(bool all = false)
        {
            var count = 0;
            Messenger.Message($"Всего файлов в сборке: {assembly.Count}");
            Messenger.Message($"Всего файлов в исходниках: {source.Count}");
            Messenger.Message($"Найдено модов: {Core.Count}");
            Messenger.Message($"Найдено файлов: {countFile}");
            //Messenger.Message($"Не найдено файлов: {Core.["Not found"].Count}");
           // Messenger.Message($"Проверка: Найдено + Ненайдено = {countFile + Result["Not found"].Count}");
            if (all)
                foreach (var mod in Core)
                {
                    Messenger.Message(string.Format($"\nИмя мода: {mod.NameMod}\n") + new string('-', 40));

                    foreach (var file in mod.AssemblyDictionary)
                    {
                        Messenger.Message($"Файлы в моде: {file.Key}");
                        if (++count % 10 == 0) Console.ReadKey();
                    }
                }
        }
    }
}
