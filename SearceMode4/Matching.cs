using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    internal class Matching
    {

        private readonly Assembly assembly;
        private readonly Source source;

        protected Matching()
        { }

        internal Matching(Assembly assembly, Source source)
        {
            this.assembly = assembly;
            this.source = source;
            if (Result == null)
                Create();
        }

        internal Matching(string pathAssembly, string pathSource)
        {
            assembly = new Assembly(pathAssembly);
            source = new Source(pathSource);
            if (Result == null)
                Create();
        }
        
        protected Dictionary<string, Dictionary<string, string>> Result { get; private set; }              

        private int countFile;

        protected void Create()
        {
            Result = new Dictionary<string, Dictionary<string, string>>();
            countFile = 0;
            foreach (var assemblyItem in assembly)
            {
                var flagNotFound = false;
                foreach (var sourceItem in source)
                {
                    if (string.Compare(assemblyItem.EntryPath, sourceItem.EntryPath, true) == 0 && !Result.ContainsKey(sourceItem.NameMod))
                    {
                        Result.Add(sourceItem.NameMod, new Dictionary<string, string> { { assemblyItem.EntryPath, assemblyItem.SpecialPath } });
                        flagNotFound = true;
                        countFile++;
                        break;
                    }
                    if (string.Compare(assemblyItem.EntryPath, sourceItem.EntryPath, true) == 0 && Result.ContainsKey(sourceItem.NameMod))
                    {
                        Result[sourceItem.NameMod].Add(assemblyItem.EntryPath, assemblyItem.SpecialPath);
                        flagNotFound = true;
                        countFile++;
                        break;
                    }
                }
                if (!flagNotFound && !Result.ContainsKey("Not found"))
                {
                    Result.Add("Not found", new Dictionary<string, string> { { assemblyItem.EntryPath, assemblyItem.SpecialPath } });
                    continue;
                }

                if (!flagNotFound && Result.ContainsKey("Not found"))
                {
                    Result["Not found"].Add(assemblyItem.EntryPath, assemblyItem.SpecialPath);
                }
            }
        }

        public void Display(bool all = false)
        {
            var count = 0;
            Messenger.Message($"Всего файлов в сборке: {assembly.Count}");
            Messenger.Message($"Всего файлов в исходниках: {source.Count}");
            Messenger.Message($"Найдено модов: {Result.Count}");
            Messenger.Message($"Найдено файлов: {countFile}");
            Messenger.Message($"Не найдено файлов: {Result["Not found"].Count}");
            Messenger.Message($"Проверка: Найдено + Ненайдено = {countFile + Result["Not found"].Count}");
            if (all)
                foreach (var mod in Result)
                {
                    Messenger.Message(string.Format($"\nИмя мода: {mod.Key}\n") + new string('-', 40));

                    foreach (var file in Result[mod.Key])
                    {
                        Messenger.Message($"Файлы в моде: {file.Key}");
                        if (++count % 10 == 0) Console.ReadKey();
                    }
                }
        }
    }
}
