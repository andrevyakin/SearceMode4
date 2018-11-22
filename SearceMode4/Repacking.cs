using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZip;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace SearceMode4
{
    class Repacking
    {
        public static void RepackingRar(string path)
        {
            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Environment.CurrentDirectory, @"x64\", "7z.dll")
                : Path.Combine(Environment.CurrentDirectory, @"x86\", "7z.dll"));
            
            using (var archive = RarArchive.Open(path))
            {
                FileStream fs = new FileStream(@"D:\Skyrim LE\Test.7z", FileMode.Create);
                
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteTo(fs);

                    SevenZipCompressor szc = new SevenZipCompressor
                    {
                        CompressionMethod = CompressionMethod.Lzma,
                        CompressionLevel = CompressionLevel.Normal,
                        CompressionMode = CompressionMode.Create,
                        DirectoryStructure = true,
                        PreserveDirectoryRoot = false,
                        ArchiveFormat = OutArchiveFormat.SevenZip
                    };


                    if (fs != null)
                    {
                        szc.CompressFileDictionary(new Dictionary<string, string> {{entry.Key, entry.Key}}, fs);
                    }

                }
            }

        }
            
       
    }
}
