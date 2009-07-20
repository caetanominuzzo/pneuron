using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace primeira.pNeuron.Editor.Business
{
    public static partial class FileManager
    {
        public static string LastWrite(TimeSpan time)
        {
            if ((int)time.TotalDays == 1)
                return "Yesterday";
            else if (time.TotalDays >= 2)
                return string.Format("{0} days ago", (int)time.TotalDays);

            else if ((int)time.TotalHours == 1)
                return string.Format("One hour ago");
            else if (time.TotalHours >= 2)
                return string.Format("{0} hours ago", (int)time.TotalHours);

            else if (time.TotalMinutes >= 2)
                return string.Format("{0} minutes ago", (int)time.TotalMinutes);
            else
                return "recently saved";
        }

        public static string GetNewFile(DocumentDefinition fileVersion, string basedir)
        {
            string result = string.Empty;

            for (int i = 1; i < 100; i++)
            {
                string name = string.Format(fileVersion.DefaultName, i);

                if (!File.Exists(string.Format("{0}{1}{2}", basedir, name, fileVersion.Extension)))
                {
                    result = string.Format("{0}{1}", name, fileVersion.Extension);
                    break;
                }
            }
            return result;
        }

        public static string MeasureFolderPath(string value, Font f, Size Size)
        {
            char[] ss = new char[value.Length];

            value.CopyTo(0, ss, 0, value.Length);

            string s = new string(ss);

            TextRenderer.MeasureText(s, f, Size,
            TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);

            return s;
        }

        #region Recent file

        public static IRecentFileManager Recent { get; private set; }

        public static void SetRecentManager(IRecentFileManager recentManager)
        {
            Recent = recentManager;
        }

        #endregion

    }
}
