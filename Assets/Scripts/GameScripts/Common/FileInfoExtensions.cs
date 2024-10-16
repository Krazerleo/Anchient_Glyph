using System.IO;

namespace AncientGlyph.GameScripts.Common
{
    public static class FileInfoExtensions
    {
        public static bool HasExtension(this FileInfo file, string[] extensions)
        {
            foreach (string extension in extensions)
            {
                if (file.Extension == extension)
                {
                    return true;
                }
            }

            return false;
        }
    }
}