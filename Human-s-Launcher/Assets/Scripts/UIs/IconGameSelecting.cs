using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HumansLancher.Models;

namespace HumansLancher.UIs
{
    public class IconGameSelecting : MonoBehaviour
    {
        private Image iconSelectingGame;        
        private List<Sprite> Icons = new List<Sprite>();

        [SerializeField] Sprite defaultSprite = null;

        // Start is called before the first frame update
        void Start()
        {
            iconSelectingGame = gameObject.GetComponent<Image>();
            var lancher = GameObject.Find("LancherManager").GetComponent<LancherManager>();            

            foreach(var gameData in lancher.gameDatas)
            {
                Texture2D texture = Resources.Load(gameData.IconPath) as Texture2D;

                Sprite sprite = (texture == null) ? 
                    defaultSprite : 
                    Sprite.Create(
                        texture,
                        new Rect(0f, 0f, texture.width, texture.height),
                        Vector2.zero);

                Icons.Add(sprite);
                
            }

            lancher.selectingNumChanged += new EventHandler((s, e) => SettingIcon());
            
            SettingIcon();
        }

        void SettingIcon()
        {
            var lancher = GameObject.Find("LancherManager").GetComponent<LancherManager>();
            iconSelectingGame.type = Image.Type.Filled;
            iconSelectingGame.sprite = Icons[lancher.SelectingNum];
        }
    }
}
