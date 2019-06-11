using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneralPurposeLibrary.FileSystem
{

    public static class FileSystems
    {
        //Begin  Experimental
        public static class File
        {
            public static class Write
            {
                public static class Clean
                {
                    public static Boolean Text(String fullPath, String content)
                    {
                        Boolean ReturnValue = false;

                        try
                        {
                            if (System.IO.File.Exists(fullPath) == true)
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            System.IO.File.WriteAllText(fullPath, content);

                            ReturnValue = true;
                        }
                        catch (Exception ex)
                        {
                            ReturnValue = false;
                        }

                        return ReturnValue;
                    }

                    public static class Ordered
                    {
                        public static void AllLines(String fullPath, HashSet<String> content)
                        {
                            List<String> sContent = new List<String>(content);
                            List<String> OrderedContent = sContent.OrderBy(x => x).ToList();

                            StringBuilder sb = new StringBuilder();

                            for (int iElement = 0; iElement < OrderedContent.Count; iElement++)
                            {
                                sb.AppendLine(OrderedContent[iElement]);
                            }
                            FileSystems.File.Write.Clean.Text(fullPath, sb.ToString());
                        }
                    }
                }
            }

            public static class Create
            {
                public static class Filename
                {
                    public static String FormatWithTime(String prefix, String extension)
                    {
                        return $"{prefix}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.{extension}";
                    }
                }
            }

            public static class Delete
            {
                /// <summary>
                /// Delete and refresh all files inside folder <b>di</b>
                /// </summary>
                /// <param name="di"></param>
                public static void Refresh(DirectoryInfo di)
                {
                    if (di.Exists == true)
                    {
                        File.Delete.Refresh(di.GetFiles());
                    }
                }
                /// <summary>
                /// Delete and refresh all files <b>fis</b>
                /// </summary>
                /// <param name="fis"></param>
                public static void Refresh(FileInfo[] fis)
                {
                    foreach (FileInfo fi in fis)
                    {
                        if (fi.Exists == true)
                        {
                            File.Delete.Refresh(fi);
                        }
                    }
                }
                /// <summary>
                /// Delete and refresh file <b>fi</b>
                /// </summary>
                /// <param name="fi"></param>
                public static void Refresh(FileInfo fi)
                {
                    if (fi.Exists == true)
                    {
                        fi.Refresh();
                        fi.Delete();
                        fi.Refresh();
                    }
                }
            }

            public static class Copy
            {
                public static int ToFolder(FileInfo sourcePath,
                                            DirectoryInfo targetPath)
                {
                    int ReturnValue = 0;

                    if (File.Exist(sourcePath) == true)
                    {
                        if (Folder.Exist(targetPath) == false)
                        {
                            Folder.Create.Clear(targetPath);
                        }

                        String targetFullPath = String.Format(@"{0}\{1}", targetPath.FullName, sourcePath.Name);
                        FileInfo fi = new FileInfo(targetFullPath);
                        if (File.Exist(fi) == true)
                        {
                            File.Delete.Refresh(fi);
                        }
                        ReturnValue = ToPath(sourcePath, fi);
                    }
                    return ReturnValue;
                }

                public static int ToFolder(FileInfo[] sourcesPath,
                                            DirectoryInfo targetPath)
                {
                    int ReturnValue = 0;

                    foreach (FileInfo s in sourcesPath)
                    {
                        File.Copy.ToFolder(s, targetPath);

                        ReturnValue++;
                    }

                    return ReturnValue;
                }

                public static int ToPath(FileInfo sourcePath, FileInfo targetPath)
                {
                    int ReturnValue = 0;

                    if (File.Exist(sourcePath) == true)
                    {
                        if (File.Exist(targetPath) == true)
                        {
                            File.Delete.Refresh(targetPath);
                        }

                        System.IO.File.Copy(sourcePath.FullName, targetPath.FullName, true);
                        ReturnValue++;
                    }



                    return ReturnValue;
                }
            }

            public static Boolean Exist(FileInfo fi)
            {
                Boolean ReturnValue = false;

                fi.Refresh();
                ReturnValue = fi.Exists;

                return ReturnValue;
            }

            public static class Path
            {
                public static FileInfo Combine(String pathBefore, String pathAfter)
                {
                    return new FileInfo(System.IO.Path.Combine(pathBefore, pathAfter));
                }

                //public static FileInfo Combine(DirectoryInfo pathBefore, FileInfo pathAfter)
                //{
                //    return new FileInfo(System.IO.Path.Combine(pathBefore.FullName, pathAfter.Name));
                //}
            }
        }

        public static class Folder
        {
            public static class Delete
            {
                public static void Refresh(DirectoryInfo di)
                {
                    if (di.Exists == true)
                    {
                        di.Refresh();
                        di.Delete();
                        di.Refresh();
                    }
                }         

                /// <summary>
                /// Delete all file and folder under  <b>folderName</b> and the delete  <b>folderName</b>
                /// </summary>
                /// <param name="folderName"></param>
                public static Boolean Clear(string folderName)
                {
                    Boolean ReturnValue = false;
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(folderName);

                        ReturnValue = Clear(di);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = false;
                    }

                    return ReturnValue;
                }
                /// <summary>
                /// Delete all file and folder under  <b>di</b> and the delete  <b>di</b>
                /// </summary>
                /// <param name="di"></param>
                public static Boolean Clear(DirectoryInfo di)
                {
                    Boolean ReturnValue = false;
                    try
                    {
                        if (di.Exists == true)
                        {
                            Clean(di);
                            Refresh(di);
                            ReturnValue = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = false;
                    }

                    return ReturnValue;
                }

                /// <summary>
                /// Delete all file and folder under  <b>folderName</b>
                /// </summary>
                /// <param name="folderName"></param>
                public static Boolean Clean(string folderName)
                {
                    Boolean ReturnValue = false;
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(folderName);

                        ReturnValue = Clean(di);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = false;
                    }

                    return ReturnValue;
                }

                /// <summary>
                /// Delete all file and folder under  <b>di</b>
                /// </summary>
                /// <param name="di"></param>
                public static Boolean Clean(DirectoryInfo di)
                {
                    Boolean ReturnValue = false;
                    try
                    {
                        if (di.Exists == true)
                        {

                            File.Delete.Refresh(di);

                            foreach (DirectoryInfo subDi in di.GetDirectories())
                            {
                                Clean(subDi);
                                Refresh(subDi);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = false;
                    }

                    return ReturnValue;
                }

            }

            public static class Create
            {
                public static Boolean Clear(DirectoryInfo di)
                {
                    Boolean ReturnValue = false;

                    Delete.Clear(di);
                    di.Create();
                    di.Refresh();

                    return ReturnValue;
                }
            }

            public static class Copy
            {
                public static int FilesFromOneFolderToAnother(String sourcePath, String targetPath)
                {
                    int ReturnValue = 0;

                    try
                    {

                        DirectoryInfo diSource = new DirectoryInfo(sourcePath);
                        DirectoryInfo diTarget = new DirectoryInfo(targetPath);

                        ReturnValue = FilesFromOneFolderToAnother(diSource, diTarget);

                    }
                    catch (Exception ex)
                    {
                        ReturnValue = -1;
                    }


                    return ReturnValue;
                }

                public static int FilesFromOneFolderToAnother(DirectoryInfo sourcePath, DirectoryInfo targetPath)
                {
                    int ReturnValue = 0;

                    try
                    {
                        ReturnValue = File.Copy.ToFolder(sourcePath.GetFiles(), targetPath);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = -1;
                    }

                    return ReturnValue;
                }

            }


            public static Boolean Exist(string folderName)
            {
                return Exist(new DirectoryInfo(folderName));
            }

            public static Boolean Exist(DirectoryInfo di)
            {
                di.Refresh();
                return di.Exists;
            }

            /// <summary>
            /// Delete all file and folder under  <b>folderName</b>
            /// </summary>
            /// <param name="folderName"></param>
            public static void ClearFolder(string folderName)
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
                        ClearFolder(di.FullName);
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
                    ClearFolder(di.FullName);
                    di.Refresh();
                    di.Delete();
                    di.Refresh();
                }
            }
        }



    }


    
}