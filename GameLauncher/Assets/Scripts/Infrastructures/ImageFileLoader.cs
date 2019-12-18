using System;
using System.IO;
using UnityEditor;

namespace GameLauncher.Infrastructures
{
    public static class ImageFileLoader
    {
        public static byte[] LoadAsBinary(string path)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var binReader = new BinaryReader(fileStream))
                    {
                        return binReader.ReadBytes((int)binReader.BaseStream.Length);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");

                return null;
            }
            catch (ArgumentException e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスが空欄です。\n" + e.Message, "OK");

                return null;
            }

        }
    }
}
