using System.IO;

namespace LibConf.Utils
{
    internal static class IOUtils
    {
        internal static void EnsureDirectory(string path)
        {
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
        }

        internal static bool IsFileReady(string path)
        {
            try
            {
                using (var file = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
