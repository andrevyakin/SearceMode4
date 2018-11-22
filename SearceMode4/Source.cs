using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearceMode4
{


    internal class Source : Collection
    {
        internal Source(string absolutePath) : base(absolutePath)
        {           
        }

        protected override void Create()
        {
            PathIdentification();

            if (Core != null) return;

            var preprocessing = new List<Element>();

            foreach (var item in AllFilesDir)
            {
                try
                {
                    switch (Path.GetExtension(item))
                    {
                        //Оборабатываю архив
                        case ".7z":
                        case ".zip":
                        case ".rar":
                            {
                                var files = GetFilesArch.GetFiles(Path.Combine(AbsolutePath, item));
                                foreach (var file in files)
                                    preprocessing.Add(new Element(Path.GetFileNameWithoutExtension(item), file, Path.GetDirectoryName(item)));
                                break;
                            }
                        //Обрабатываю распакованные моды
                        default:
                            {
                                if (item.IndexOf('\\') != -1)
                                    preprocessing.Add(new Element(item.Remove(item.IndexOf('\\')), item.Substring(item.IndexOf('\\') + 1), string.Empty));
                                else
                                    Messenger.Message($"{item}\nНе является архивом или распакованным модом...");
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Messenger.Message(e.ToString());
                }
            }

            Core = new List<Element>();
            var noRepet = new List<string>();
            var assemblyRootDirs = RootAssembly.GetInstance().RootDirsAssembly;
            var assemblyRootFiles = RootAssembly.GetInstance().RootFilesAssembly;

            foreach (var item in preprocessing)
            {
                var itemSplit = item.EntryPath.Split('\\');
                var itemJoin = new List<string>();
                var flagRootFile = false;
                var flagPathInMod = false;

                for (var index = 0; index < itemSplit.Length; index++)
                {
                    var word = itemSplit[index];

                    //Если последнее слово - это имя корневого файла, добавляю его в базу
                    if (index == itemSplit.Length - 1)
                    {
                        foreach (var file in assemblyRootFiles)
                        {
                            if (string.Compare(word, file, true) != 0 ||
                                noRepet.Contains(word)) continue;
                            noRepet.Add(word);
                            Core.Add(new Element(item.NameMod, word, item.SpecialPath));                            
                            flagRootFile = true;
                            break;
                        }
                    }

                    //Пропускаю все слова, которые не равны директориям из сборки
                    if (!flagPathInMod && !flagRootFile)
                        foreach (var rootDir in assemblyRootDirs)
                            if (string.Compare(word, rootDir, true) == 0)
                                flagPathInMod = true;

                    //Собираю все слова, начиная с директорий в сборке
                    if (flagPathInMod)
                        itemJoin.Add(word);
                }
                var relativePathJoin = string.Join("\\", itemJoin);

                if (relativePathJoin == string.Empty || noRepet.Contains(relativePathJoin)) continue;
                noRepet.Add(relativePathJoin);
                Core.Add(new Element(item.NameMod, relativePathJoin, item.SpecialPath));
            }
        }
    }
}
