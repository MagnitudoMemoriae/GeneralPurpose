using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneralPurposeLibrary.FileSystem
{
    public static class FileSystem
    {
        /// <summary>
        /// Delete all file and folder under  <b>folderName</b>
        /// </summary>
        /// <param name="folderName"></param>
        public static void ClearSubFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);

            if (dir.Exists == true)
            {
                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.Refresh();
                    fi.Delete();
                    fi.Refresh();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    ClearSubFolder(di.FullName);
                    di.Refresh();
                    di.Delete();
                    di.Refresh();
                }
            }
        }

        /// <summary>
        /// Delete all file and folder under  <b>folderName</b> and the delete  <b>folderName</b>
        /// </summary>
        /// <param name="folderName"></param>
        public static void DeleteFolder(string folderName)
        {
            DirectoryInfo di = new DirectoryInfo(folderName);

            if (di.Exists == true)
            {
                ClearSubFolder(di.FullName);
            }
            di.Refresh();
            di.Delete();
            di.Refresh();
        }
    }
}
