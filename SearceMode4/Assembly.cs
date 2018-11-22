using System;
using System.Collections.Generic;
using System.IO;

namespace SearceMode4
{
    internal class Assembly : Collection
    {
        internal Assembly(string absolutePath) : base(absolutePath)
        {
        }

        protected override void Create()
        {
            PathIdentification();

            if (Core == null)
            {
                Core = new List<Element>();
                //Формирую основую базу
                foreach (var file in AllFilesDir)
                    if (file.StartsWith("Data", StringComparison.OrdinalIgnoreCase))
                        Core.Add(new Element("Data", file.Substring(5), Path.Combine(AbsolutePath, file.Substring(5))));
            }

            var rootDirsAssembly = new List<string>();
            var rootFilesAssembly = new List<string>();

            foreach (var item in Core)
            {
                //Получаю корневые директории сборки, нужны для обработки путей Исходников
                if (item.EntryPath.Split('\\').Length > 1 && !rootDirsAssembly.Contains(item.EntryPath.Split('\\')[0]))
                    rootDirsAssembly.Add(item.EntryPath.Split('\\')[0]);
                //Получаю корневые файлы сборки, нужны для обработки путей Исходников
                if (item.EntryPath.Split('\\').Length == 1)
                    rootFilesAssembly.Add(item.EntryPath);
            }
            
            //Записываю корневые файлы и папки в синглтон
            RootAssembly.GetInstance(rootDirsAssembly, rootFilesAssembly);
        }
    }
}
