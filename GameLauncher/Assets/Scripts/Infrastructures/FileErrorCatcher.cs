using System;
using System.IO;
using UnityEditor;

namespace GameLauncher.Infrastructures
{
    public static class FileErrorCatcher
    {
        public static string[] CatchError(Func<string, string[]> func, string argument)
        {
            string[] errors = new string[3];

            try
            {
                return func(argument);
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");
                errors[0] = e.Message;
                return errors;
            }
            catch (ArgumentException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスが空欄です。\n" + e.Message, "OK");
                errors[0] = e.Message;
                return errors;
            }
        }

        public static string CatchError(Func<string, string> func, string argument)
        {            
            try
            {
                return func(argument);
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");
                return e.Message;
            }
            catch (ArgumentException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスが空欄です。\n" + e.Message, "OK");
                return e.Message;
            }
        }
    }
}
