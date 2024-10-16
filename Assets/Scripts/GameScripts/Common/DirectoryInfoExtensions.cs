using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AncientGlyph.GameScripts.Common
{
    public static class DirectoryInfoExtensions
    {
        public static List<FileInfo> GetFilesRecursive(this DirectoryInfo directoryInfo, string namePattern)
        {
            List<FileInfo> foundFiles = new();
            directoryInfo.GetFilesRecursiveInternal(namePattern, foundFiles);
            return foundFiles;
        }

        public static List<FileInfo> GetFilesWithExtension(this DirectoryInfo directoryInfo, params string[] extensions) =>
            extensions.SelectMany(extension => directoryInfo.GetFiles($"*.{extension}")).ToList();

        private static void GetFilesRecursiveInternal(this DirectoryInfo directoryInfo, string namePattern,
                                                      List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directoryInfo.GetFiles(namePattern));

            foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
            {
                subDirectory.GetFilesRecursiveInternal(namePattern, foundFiles);
            }
        }
    }
}