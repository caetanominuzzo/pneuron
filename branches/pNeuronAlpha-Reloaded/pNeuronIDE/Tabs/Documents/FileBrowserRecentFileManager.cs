using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace primeira.pNeuron
{
    internal class FileBrowserRecentFileManager
    {
        private static string path = "recent.txt";

        public static void Add(string FilePath)
        {
            try
            {
                if (!Get().Contains(FilePath))
                    File.AppendAllText(path, "\n" + FilePath);
            }
            catch { }
        }

        public static string[] Get()
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch { return new string[0]; }
        }
    }
}
