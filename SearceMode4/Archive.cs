using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZip;

namespace SearceMode4
{
    internal class Archive : Matching
    {
        private readonly string pathResult;

        internal Archive(string pathResult, Assembly assembly, Source source) : base(assembly, source)
        {
            this.pathResult = pathResult;
            Create();
        }

        internal Archive(string pathResult, string pathAssembly, string pathSource) : base(pathAssembly, pathSource)
        {
            this.pathResult = pathResult;
            Create();
        }

        private new void Create()
        {
            base.Create();

            if (Result == null)
            {
                Messenger.Message("Что-то пошло не так...");
                return;
            }

            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Environment.CurrentDirectory, @"x64\", "7z.dll")
                : Path.Combine(Environment.CurrentDirectory, @"x86\", "7z.dll"));

            foreach (var mod in Result)
            {
                if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(pathResult, mod.Key)) ?? throw new InvalidOperationException()))
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(pathResult, mod.Key)) ?? throw new InvalidOperationException());

                using (FileStream fs = new FileStream(string.Concat(pathResult, '\\', mod.Key, ".7z"), FileMode.Create))
                {
                    SevenZipCompressor szc = new SevenZipCompressor
                    {
                        CompressionMethod = CompressionMethod.Lzma,
                        CompressionLevel = CompressionLevel.Normal,
                        CompressionMode = CompressionMode.Create,
                        DirectoryStructure = true,
                        PreserveDirectoryRoot = false,
                        ArchiveFormat = OutArchiveFormat.SevenZip
                    };
                        //if (fs != null)
                            szc.CompressFileDictionary(mod.Value, fs);
                }
            }
        }
    }
}
