using Assets.Scripts.Infrastructures;
using System;
using System.IO;

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
                MessageBoxShower.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");

                return null;
            }
            catch (ArgumentException e)
            {
                MessageBoxShower.DisplayDialog("❌Error", "ファイルパスが空欄です。\n" + e.Message, "OK");

                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBoxShower.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");

                return null;

            }
        }
    }
}
