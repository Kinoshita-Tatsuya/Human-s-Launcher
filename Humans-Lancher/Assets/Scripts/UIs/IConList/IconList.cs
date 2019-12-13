﻿using HumansLancher.Models.Executable;
using HumansLancher.Services.Factory;
using HumansLancher.Services.GameDataDAO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HumansLancher.UIs.IconList
{
    public class IconList : MonoBehaviour
    {
        [SerializeField] Sprite defaultSprite = null;
        [SerializeField] Icon iconPrefab = null;
        [SerializeField] GameObject frame = null;

        void Start()
        {
            iconLocator = GameObject.Find("IconLocator").GetComponent<IconLocator_rail>();

            IGameDataDAO gameDataDAO = new GameDataDAO();
            var gameDatas = gameDataDAO.GetGameDatas();

            foreach (var gameData in gameDatas)
            {
                ISpriteFactory spriteFactory = new SpriteFactory();
                var sprite = spriteFactory.Create(gameData.TexturePath, defaultSprite);
                AbstractExecutable executable = new Executable(gameData.ExePath);

                var icon = Icon.Instantiate(iconPrefab, gameData.Title, sprite, executable);
                icons.Add(icon);
            }

            iconLocator.InitPos(icons);

            icons[0].OnAnimStarted += (_)=> { DisableSelecting(); };
            icons[0].OnAnimEnded += (_) => { EnableSelecting(); };
        }

        void Update()
        {
           
        }

        public AbstractExecutable SelectingExecutable
        {
            get
            {
                if (icons.Count < 0)
                {
                    return null;
                }
                else
                {
                    if(canExecute)
                    {
                        return icons[0].Executable;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void ToNext()
        {
            iconLocator.ToNext(icons);

            icons[0].OnAnimEnded += UpdatePos;
        }

        public void ToPrev()
        {
            iconLocator.ToPrev(icons);

            icons[0].OnAnimEnded += UpdatePos;
        }

        void UpdatePos(Icon icon)
        {
            iconLocator.UpdatePos(icons);

            icon.OnAnimEnded -= UpdatePos;
        }

        void EnableSelecting()
        {
            canExecute = true;
            frame.SetActive(true);
        }

        void DisableSelecting()
        {
            canExecute = false;
            frame.SetActive(false);
        }

        IconLocator_rail iconLocator;

        bool canExecute = true;

        List<Icon> icons = new List<Icon>();
    }
}
