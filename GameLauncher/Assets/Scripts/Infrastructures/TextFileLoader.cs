﻿using System;
using System.IO;
using UnityEditor;

namespace GameLauncher.Infrastructures
{

    public static class TextFileLoader
    {
        public static string Get(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (FileNotFoundException e)
            {                
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");

                return e.Message;
            }
            catch (ArgumentException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスが空欄です。\n" + e.Message, "OK");

                return e.Message;
            }
            catch (DirectoryNotFoundException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");

                return e.Message;

            }
        }
    }       
}
