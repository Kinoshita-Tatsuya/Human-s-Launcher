using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HumansLancher.Services.Factory
{
    public class SpriteFactory : ISpriteFactory
    {
        public Sprite Create(string texturePath, Sprite defaultSprite)
        {
            Texture2D texture = LeadBinary(ReadImageFileAsBinary(texturePath));

            Sprite sprite = (texture == null) ?
                    defaultSprite :
                    Sprite.Create(
                        texture,
                        new Rect(0f, 0f, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f));

            return sprite;
        }

        public byte[] ReadImageFileAsBinary(string path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader bin = new BinaryReader(fileStream);
                    byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);
                    bin.Close();
                    return values;
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

        public Texture2D LeadBinary(byte[] binary)
        {
            if (binary == null) return null;
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(binary);
            return texture;
        }
    }
}
