using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HumansLancher.Services.Factory
{
    public interface ISpriteFactory
    {
        Sprite Create(string texturePath, Sprite defaultSprite);
    }
}
