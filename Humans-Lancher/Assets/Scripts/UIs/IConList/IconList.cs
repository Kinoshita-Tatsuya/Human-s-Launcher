using HumansLancher.Models.Executable;
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

        void Start()
        {
            iconLocator = GameObject.Find("IconLocator").GetComponent<IIconLocator>();

            iconLocator.OnMovingStarted += DisableSelecting;
            iconLocator.OnMovingEnded += EnableSelecting;

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
        }

        void Update()
        {
            iconLocator.UpdatePos(icons);
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
        }

        public void ToPrev()
        {
            iconLocator.ToPrev(icons);
        }

        void EnableSelecting()
        {
            canExecute = true;
        }

        void DisableSelecting()
        {
            canExecute = false;
        }

        IIconLocator iconLocator;

        bool canExecute = true;

        List<Icon> icons = new List<Icon>();
    }
}
