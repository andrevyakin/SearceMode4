using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace SearceMode4
{
    class Repacking
    {
        public static void RepackingRar(string path)
        {
            using (var archive = RarArchive.Open(path))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                   
                }
            }
        }
    }
}
