using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SevenZip;
using SharpCompress.Archives.Rar;


namespace SearceMode4
{
    internal static class GetFilesArch
    {
        internal static List<string> GetFiles(string pathArchive)
        {
            var reedArchiveResult = new List<string>();

            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Environment.CurrentDirectory, @"x64\", "7z.dll")
                : Path.Combine(Environment.CurrentDirectory, @"x86\", "7z.dll"));

            try
            {
                switch (Path.GetExtension(pathArchive))
                {
                    case ".7z":
                        using (var archive = new SevenZipExtractor(pathArchive))
                            foreach (var entry in archive.ArchiveFileData.Where(entry => !entry.IsDirectory))
                                    reedArchiveResult.Add(entry.FileName);
                        break;

                    case ".zip":
                        using (var archive = ZipFile.OpenRead(pathArchive))
                            foreach (var entry in archive.Entries)
                                reedArchiveResult.Add(entry.FullName);
                        break;

                    case ".rar":
                        using (var archive = RarArchive.Open(pathArchive))
                            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                                reedArchiveResult.Add(entry.Key);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Не удалось прочесть {pathArchive}");
            }

            return reedArchiveResult;
        }
    }
}
