using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearceMode4
{
    [Serializable]
    public abstract class Collection : IList<Element>
    {        

        public Collection()
        { }

        protected Collection(string absolutePath)
        {
            AbsolutePath = absolutePath;
            Create();
        }

        protected string AbsolutePath { get; }


        public List<Element> Core { get; protected set; }


        protected List<string> AllFilesDir { get; private set; }

        [XmlIgnore]
        public int Count => ((IList<Element>)Core).Count;
        [XmlIgnore]
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
                        default:
                            Messenger.Message("Некорректный тип файла...");
                            break;
                    }
                }
                else
                    Messenger.Message($"Некорректный путь {AbsolutePath}");
            }
            catch (Exception e)
            {
                Messenger.Message($"Ошибка пути к каталогу: {e.Message}");
            }
        }

        protected abstract void Create();          

        public void Display()
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
