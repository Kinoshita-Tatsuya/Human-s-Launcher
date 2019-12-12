using System.Collections;
using UnityEngine;

namespace HumansLancher.Services.Factory
{
    public class SpriteFactory : ISpriteFactory
    {
        public Sprite Create(string texturePath, Sprite defaultSprite)
        {
            Texture2D texture = Resources.Load(texturePath) as Texture2D;

            Sprite sprite = (texture == null) ?
                    defaultSprite :
                    Sprite.Create(
                        texture,
                        new Rect(0f, 0f, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f));

            return sprite;
        }
    }
}
