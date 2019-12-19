using System.IO;

namespace GameLauncher.Infrastructures
{
    public static class FileDataProvider
    {
        public static string[] GetFolderNames(string path)
        {
            return Directory.GetDirectories(path);
        }
    }
}
