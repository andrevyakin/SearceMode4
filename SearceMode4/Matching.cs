using System;
using System.Collections.Generic;

namespace SearceMode4
{
    internal class Matching
    {

        private readonly Assembly assembly;
        private readonly Source source;
        
        internal Matching(Assembly assembly, Source source)
        {
            this.assembly = assembly;
            this.source = source;
            if(Result == null)
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
                    if (sourceItem.SpecialPath != null && (string.Compare(assemblyItem.EntryPath, sourceItem.EntryPath,
                                                               StringComparison.OrdinalIgnoreCase) == 0 && !Result.ContainsKey(sourceItem.SpecialPath)))
                    {
                        Result[sourceItem.SpecialPath] = new Dictionary<string, string>{{assemblyItem.EntryPath, assemblyItem.SpecialPath}};
                        flagNotFound = true;
                        countFile++;
                        break;
                    }

                    if (string.Compare(assemblyItem.EntryPath, sourceItem.EntryPath,
                            StringComparison.OrdinalIgnoreCase) == 0 && Result.ContainsKey(sourceItem.SpecialPath ?? throw new InvalidOperationException()))
                    {
                        Result[sourceItem.SpecialPath].Add(assemblyItem.EntryPath, assemblyItem.SpecialPath);
                        flagNotFound = true;
                        countFile++;
                        break;
                    }
                }

                if (!flagNotFound && !Result.ContainsKey("Not found"))
                {
                    Result["Not found"] = new Dictionary<string, string> { { assemblyItem.EntryPath, assemblyItem.SpecialPath } };
                    continue;
                }
                if (!flagNotFound && Result.ContainsKey("Not found"))
                    Result["Not found"].Add(assemblyItem.EntryPath, assemblyItem.SpecialPath);
            }
        }

        public void Display(bool all = false)
        {
            var count = 0;
            Messenger.Message($"Всего файлов в сборке: {assembly.Count}");
            Messenger.Message($"Всего файлов в исходниках: {source.Count}");
            Messenger.Message($"Найдено модов: {Result.Count}");
            Messenger.Message($"Найдено файлов: {countFile}");
            if (Result.ContainsKey("Not found"))
            Messenger.Message($"Не найдено файлов: {Result["Not found"].Count}");
            if (Result.ContainsKey("Not found"))
                Messenger.Message($"Проверка: Найдено + Ненайдено = {countFile + Result["Not found"].Count}");
            if (all)
                foreach (var mod in Result)
                {
                    Messenger.Message(string.Format($"\nИмя мода: {mod.Key}\n") + new string('-', 40));

                    foreach (var file in mod.Value)
                    {
                        Messenger.Message($"Файлы в моде: {file.Key}");
                        if (++count % 10 == 0)
                        {
                            if(Console.ReadKey().Key==ConsoleKey.Enter)
                                break;
                            Messenger.Message(string.Format($"\nИмя мода: {mod.Key}\n") + new string('-', 40));
                        }
                    }
                }
        }
    }
}