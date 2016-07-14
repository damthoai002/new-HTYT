using System.IO;

namespace UKPI.Utils
{
    public class FileHelper
    {
        public static void EnsureDirectoryExist(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}
