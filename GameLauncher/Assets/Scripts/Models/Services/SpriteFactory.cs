using GameLauncher.Infrastructures;
using UnityEngine;

namespace GameLauncher.Models.Services
{
    public static class SpriteFactory
    {
        static SpriteFactory()
        {
            DefaultSprite = Resources.Load<Sprite>("DefaultSprite");
        }

        public static Sprite Create(string texturePath)
        {
            Texture2D texture = CreateTexture2D(ImageFileLoader.LoadAsBinary(texturePath));

            Sprite sprite = (texture == null) ?
                    DefaultSprite :
                    Sprite.Create(
                        texture,
                        new Rect(0f, 0f, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f));

            return sprite;
        }

        private static Texture2D CreateTexture2D(byte[] binaryImage)
        {
            if (binaryImage == null) return null;

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(binaryImage);

            return texture;
        }

        private static Sprite DefaultSprite { get; set; }
    }
}
