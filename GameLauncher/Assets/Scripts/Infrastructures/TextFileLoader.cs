using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace GameLauncher.Infrastructures
{
    public static class TextFileLoader
    {
        public static string GetAllLine(string path)
        {
            return FileErrorCatcher.CatchError(File.ReadAllText, path);
        }        
    }      
}
