using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace GeneralPurposeLibrary.FileSystem
{
    public class FileDescriptor
    {
        public readonly DirectoryInfo Folder;
        public readonly FileInfo Element;
        public readonly String BaseFolder;

        public FileDescriptor(DirectoryInfo folder, FileInfo element)
        {
            Folder = folder;
            Element = element;
        }

        public FileDescriptor(DirectoryInfo folder, FileInfo element, String baseFolder)
        {
            Folder = folder;
            Element = element;
            BaseFolder = baseFolder;
        }
    }
    public class FilesDescriptor
    {
        public readonly DirectoryInfo Folder;
        public readonly List<FileInfo> Elements;

        public FilesDescriptor(DirectoryInfo folder, List<FileInfo> elements)
        {
            Folder = folder;
            Elements = elements;
        }
    }
    public static class FileSystems
    {
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
                /// <summary>
                /// Create an empty folder
                /// </summary>
                /// <param name="di"></param>
                /// <returns></returns>
                public static Boolean Clear(DirectoryInfo di)
                {
                    Boolean ReturnValue = false;

                    Delete.Clear(di);
                    di.Create();
                    di.Refresh();

                    return ReturnValue;
                }


                /// <summary>
                /// If folder exist do nothing otherwise create it
                /// </summary>
                /// <param name="di"></param>
                /// <returns></returns>
                public static Boolean Check(DirectoryInfo di)
                {
                    Boolean ReturnValue = false;

                    if (Exist(di) == false)
                    {
                        di.Create();
                        di.Refresh();
                    }

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
        }

        public enum FileCompressionMode
        {
            DELETEFOLDERANDCREATE,
            DELETEFILES,
            CREATEFILE,
            UPDATEFILE
        }

        public static class Compression
        {
            public static class Files
            {
                public static FileInfo Execute(FilesDescriptor descriptor,
                                                    FileInfo compressedFileName,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        ReturnValue = Execute(descriptor, compressedFileName, String.Empty, mode);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }

                public static FileInfo Execute(FilesDescriptor descriptor,
                                                    FileInfo compressedFileName,
                                                    String rootFolderName,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        ReturnValue = Execute(descriptor.Elements,
                                                    compressedFileName,
                                                    rootFolderName,
                                                    mode);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }

                /// <summary>
                /// Add files to a compressed archive , if not exist will be created , if exist files
                /// </summary>
                /// <param name="files"></param>
                /// <param name="outputFolder"></param>
                /// <param name="compressedFileName"></param>
                /// <returns></returns>
                public static FileInfo Execute(List<FileInfo> files,
                                                    FileInfo compressedFile,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        ReturnValue = Execute(files, compressedFile, string.Empty, mode);
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }

                /// <summary>
                /// Add files to a compressed archive , if not exist will be created , if exist files
                /// The name of the entry file in compressed archive is preceded by rootFolderName
                /// </summary>
                /// <param name="files"></param>
                /// <param name="outputFolder"></param>
                /// <param name="rootFolderName"></param>
                /// <returns></returns>
                public static FileInfo Execute(List<FileInfo> files,
                                                    FileInfo compressedFile,
                                                    String rootFolderName,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(compressedFile.DirectoryName);
                        switch (mode)
                        {
                            case FileCompressionMode.DELETEFOLDERANDCREATE:
                                Folder.Create.Clear(directoryInfo);

                                break;

                            case FileCompressionMode.DELETEFILES:
                                Folder.Delete.Clean(directoryInfo);

                                break;

                            case FileCompressionMode.CREATEFILE:
                                Folder.Create.Check(directoryInfo);
                                File.Delete.Refresh(compressedFile);

                                break;

                            case FileCompressionMode.UPDATEFILE:
                                Folder.Create.Check(directoryInfo);
                                break;

                            default:
                                break;
                        }

                        if (File.Exist(compressedFile) == false)
                        {
                            using (ZipArchive archive = ZipFile.Open(compressedFile.FullName, ZipArchiveMode.Create))
                            {
                                archive.Dispose();
                            }
                        }

                        using (FileStream zipToOpen = new FileStream(compressedFile.FullName, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                Execute(files, archive, rootFolderName);
                            }
                        }

                        ReturnValue = compressedFile;
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }

                public static FileInfo Execute(Dictionary<String, List<FileInfo>> files,
                                                    FileInfo compressedFile,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(compressedFile.DirectoryName);
                        switch (mode)
                        {
                            case FileCompressionMode.DELETEFOLDERANDCREATE:
                                Folder.Create.Clear(directoryInfo);

                                break;

                            case FileCompressionMode.DELETEFILES:
                                Folder.Delete.Clean(directoryInfo);

                                break;

                            case FileCompressionMode.CREATEFILE:
                                Folder.Create.Check(directoryInfo);
                                File.Delete.Refresh(compressedFile);

                                break;

                            case FileCompressionMode.UPDATEFILE:
                                Folder.Create.Check(directoryInfo);
                                break;

                            default:
                                break;
                        }

                        if (File.Exist(compressedFile) == false)
                        {
                            using (ZipArchive archive = ZipFile.Open(compressedFile.FullName, ZipArchiveMode.Create))
                            {
                                archive.Dispose();
                            }
                        }

                        using (FileStream zipToOpen = new FileStream(compressedFile.FullName, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                foreach (KeyValuePair<String, List<FileInfo>> item in files)
                                {
                                    Execute(item.Value, archive, item.Key);
                                }
                            }
                        }

                        ReturnValue = compressedFile;
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }

                public static Boolean Execute(List<FileInfo> files,
                                                    ZipArchive archive,
                                                    String rootFolderName)
                {
                    Boolean ReturnValue = false;

                    try
                    {
                        for (int iFileToCompress = 0; iFileToCompress < files.Count; iFileToCompress++)
                        {
                            FileInfo FileToCompress = files[iFileToCompress];
                            String EntryName = String.Empty;

                            if (String.IsNullOrEmpty(rootFolderName) == true)
                            {
                                EntryName = FileToCompress.Name;
                            }
                            else
                            {
                                EntryName = rootFolderName + @"\" + FileToCompress.Name;
                            }

                            if (FileToCompress.Exists == true)
                            {
                                ZipArchiveEntry readmeEntry = archive.CreateEntryFromFile(FileToCompress.FullName, EntryName);
                            }
                        }

                        ReturnValue = true;
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = false;
                    }

                    return ReturnValue;
                }
            }

            public static class Folders
            {

                public static FileInfo Execute(DirectoryInfo folder,
                                                    FileInfo compressedFile,
                                                    FileCompressionMode mode)
                {
                    FileInfo ReturnValue = null;

                    ReturnValue = Execute(new List<DirectoryInfo>() { folder }, compressedFile, mode);

                    return ReturnValue;
                }

                public static FileInfo Execute(List<DirectoryInfo> folders,
                                                    FileInfo compressedFile,
                                                    FileCompressionMode mode)

                {
                    FileInfo ReturnValue = null;

                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(compressedFile.DirectoryName);
                        switch (mode)
                        {
                            case FileCompressionMode.DELETEFOLDERANDCREATE:
                                Folder.Create.Clear(directoryInfo);

                                break;

                            case FileCompressionMode.DELETEFILES:
                                Folder.Delete.Clean(directoryInfo);

                                break;

                            case FileCompressionMode.CREATEFILE:
                                Folder.Create.Check(directoryInfo);
                                File.Delete.Refresh(compressedFile);

                                break;

                            case FileCompressionMode.UPDATEFILE:
                                Folder.Create.Check(directoryInfo);
                                break;

                            default:
                                break;
                        }

                        if (File.Exist(compressedFile) == false)
                        {
                            using (ZipArchive archive = ZipFile.Open(compressedFile.FullName, ZipArchiveMode.Create))
                            {
                                archive.Dispose();
                            }
                        }

                        for (int iFolderToCompress = 0; iFolderToCompress < folders.Count; iFolderToCompress++)
                        {
                            DirectoryInfo FoldersToCompress = folders[iFolderToCompress];

                            if (FoldersToCompress.Exists == true)
                            {
                                List<FileDescriptor> descriptors = Info.Root(FoldersToCompress);

                                Dictionary<String, List<FileInfo>> dict = new Dictionary<string, List<FileInfo>>();
                                for (int iDescriptor = 0; iDescriptor < descriptors.Count; iDescriptor++)
                                {
                                    if (dict.ContainsKey(descriptors[iDescriptor].BaseFolder) == true)
                                    {
                                        dict[descriptors[iDescriptor].BaseFolder].Add(descriptors[iDescriptor].Element);
                                    }
                                    else
                                    {
                                        dict.Add(descriptors[iDescriptor].BaseFolder, new List<FileInfo>() { descriptors[iDescriptor].Element });
                                    }
                                }

                                foreach (KeyValuePair<String, List<FileInfo>> baseFolder in dict)
                                {
                                    FileSystems.Compression.Files.Execute(baseFolder.Value,
                                                                                         compressedFile,
                                                                                         baseFolder.Key,
                                                                                         FileSystems.FileCompressionMode.UPDATEFILE);
                                }
                            }
                        }

                        ReturnValue = compressedFile;
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = null;
                    }

                    return ReturnValue;
                }
            }

            public static class Info
            {
                public static List<FileDescriptor> Root(DirectoryInfo folder)
                {
                    List<FileDescriptor> ReturnValue = new List<FileDescriptor>();
                    String Offset = folder.Name;

                    FileInfo[] files = folder.GetFiles();
                    for (int iElement = 0; iElement < files.Length; iElement++)
                    {
                        FileInfo file = files[iElement];
                        FileDescriptor descriptor = new FileDescriptor(folder, file, Offset);
                        ReturnValue.Add(descriptor);
                    }

                    ReturnValue.AddRange(GetList(folder.GetDirectories().ToList(), Offset));

                    return ReturnValue;
                }

                public static List<FileDescriptor> GetList(DirectoryInfo folder, String baseFolder)
                {
                    List<FileDescriptor> ReturnValue = new List<FileDescriptor>();
                    String Offset = baseFolder + @"\" + folder.Name;

                    FileInfo[] files = folder.GetFiles();
                    for (int iElement = 0; iElement < files.Length; iElement++)
                    {
                        FileInfo file = files[iElement];
                        FileDescriptor descriptor = new FileDescriptor(folder, file, Offset);
                        ReturnValue.Add(descriptor);
                    }

                    ReturnValue.AddRange(GetList(folder.GetDirectories().ToList(), Offset));

                    return ReturnValue;
                }

                public static List<FileDescriptor> GetList(List<DirectoryInfo> elements, String baseFolder)
                {
                    List<FileDescriptor> ReturnValue = new List<FileDescriptor>();

                    //DirectoryInfo[] folders = elements.GetDirectories();
                    for (int iElement = 0; iElement < elements.Count; iElement++)
                    {
                        ReturnValue.AddRange(GetList(elements[iElement], baseFolder));
                    }

                    return ReturnValue;
                }
            }
        }


    }
}