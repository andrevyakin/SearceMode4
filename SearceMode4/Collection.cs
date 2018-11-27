using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SearceMode4
{
    public abstract class Collection : IList<Element>
    {
        // Тип для Сериализации и Десериализации.
        readonly XmlSerializer serializer = new XmlSerializer(typeof(List<Element>));
        
        protected Collection(string absolutePath)
        {
            AbsolutePath = absolutePath;
            Create();
        }

        protected string AbsolutePath { get; }

        public List<Element> Core { get; protected set; }

        protected List<string> AllFilesDir { get; private set; }

        public int Count => ((IList<Element>)Core).Count;

        public bool IsReadOnly => ((IList<Element>)Core).IsReadOnly;

        public Element this[int index] { get => ((IList<Element>)Core)[index]; set => ((IList<Element>)Core)[index] = value; }

        //Идентификация пути
        protected void PathIdentification()
        {
            try
            {
                if (Directory.Exists(AbsolutePath))
                    AllFilesDir = AllFiles.GetFilesDir(AbsolutePath);
                else if (File.Exists(AbsolutePath))
                {
                    switch (Path.GetExtension(AbsolutePath))
                    {
                        case ".txt":
                            AllFilesDir = File.ReadAllLines(AbsolutePath).ToList();
                            break;
                        case ".7z":
                        case ".zip":
                        case ".rar":
                            AllFilesDir = GetFilesArch.GetFiles(AbsolutePath);
                            break;
                        case ".xml":
                            Deserialize(AbsolutePath);
                            break;
                        default:
                            Messenger.Message($"Некорректный тип файла... {AbsolutePath}");
                            throw new ActiveDirectoryOperationException();
                    }
                }
                else
                {
                    Messenger.Message($"Некорректный путь {AbsolutePath}");
                    throw new ActiveDirectoryOperationException();
                }
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }

        protected abstract void Create();

        // СЕРИАЛИЗАЦИЯ.
        internal void Serialize(string nameFile)
        {
            using (var stream = new FileStream(nameFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                // Сохраняю объект в XML-файле на диске(СЕРИАЛИЗАЦИЯ).
                serializer.Serialize(stream, Core);
                Messenger.Message("Объект сериализован!");
            }
        }

        // ДЕСЕРИАЛИЗАЦИЯ.
        private void Deserialize(string nameFile)
        {
            if (!File.Exists(nameFile))
            {
                Messenger.Message($"Не найден файл: {nameFile}");
                return;
            }

            try
            {
                using (var stream = new FileStream(nameFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // Восстанавливаю объект из XML-файла.
                    Core = serializer.Deserialize(stream) as List<Element>;
                    Messenger.Message("Объект Десериализован!");
                }
            }
            catch (Exception ex)
            {
                Messenger.Message(ex.Message);
            }
        }

        internal virtual void Display()
        {
            var count = 0;
            foreach (Element item in Core)
            {
                Messenger.Message(item.ToString());
                if (++count % 5 == 0) Console.ReadKey();
            }
        }

        public int IndexOf(Element item)
        {
            return ((IList<Element>)Core).IndexOf(item);
        }

        public void Insert(int index, Element item)
        {
            ((IList<Element>)Core).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Element>)Core).RemoveAt(index);
        }

        public void Add(Element item)
        {
            ((IList<Element>)Core).Add(item);
        }

        public void Clear()
        {
            ((IList<Element>)Core).Clear();
        }

        public bool Contains(Element item)
        {
            return ((IList<Element>)Core).Contains(item);
        }

        public void CopyTo(Element[] array, int arrayIndex)
        {
            ((IList<Element>)Core).CopyTo(array, arrayIndex);
        }

        public bool Remove(Element item)
        {
            return ((IList<Element>)Core).Remove(item);
        }

        public IEnumerator<Element> GetEnumerator()
        {
            return ((IList<Element>)Core).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Element>)Core).GetEnumerator();
        }
    }
}