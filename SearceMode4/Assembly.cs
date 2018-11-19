using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearceMode4
{
    [Serializable]
    public class Assembly : Collection
    { 
        public Assembly()
        { }

        internal Assembly(string absolutePath) : base(absolutePath)
        {
        }          

        protected override void Create()
        {
            PathIdentification();

            Core = new List<Element>();

            var rootDirsAssembly = new List<string>();
            var rootFilesAssembly = new List<string>();

            foreach (var file in AllFilesDir)
            {
                //Формирую основую базу
                if (file.StartsWith("Data", StringComparison.OrdinalIgnoreCase))
                    Core.Add(new Element("Data", file.Substring(5), Path.Combine(AbsolutePath, file.Substring(5))));
                //Получаю корневые директории сборки, нужны для обработки путей Исходников
                if (file.StartsWith("Data", StringComparison.OrdinalIgnoreCase) && file.Split('\\').Length > 2 && !rootDirsAssembly.Contains(file.Split('\\')[1]))
                    rootDirsAssembly.Add(file.Split('\\')[1]);
                //Получаю корневые файлы сборки, нужны для обработки путей Исходников
                if (file.StartsWith("Data", StringComparison.OrdinalIgnoreCase) && file.Split('\\').Length == 2 && !rootFilesAssembly.Contains(file.Substring(5)))
                    rootFilesAssembly.Add(file.Substring(5));
            }
            //Записываю корневые файлы и папки в синглтон
            RootAssembly.GetInstance(rootDirsAssembly, rootFilesAssembly);

        }       
    }
}
