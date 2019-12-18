using GameLauncher.Models.DomainObjects;
using GameLauncher.Models.RelateIcon;
using GameLauncher.Models.RelateIcon.IconsAnimBehavior;
using GameLauncher.Models.Services;
using System.Collections.Generic;
using UnityEngine;

namespace GameLauncher.Components
{
    public class GameDataPresenter : MonoBehaviour
    {
        [SerializeField] private Icon iconPrefab = null;
        [SerializeField] private RectTransform selectingTransform = null;
        [SerializeField] private RectTransform selectingFrameTransform = null;
        [SerializeField] private RectTransform prevTransform = null;
        [SerializeField] private RectTransform prevFramTransform = null;
        [SerializeField] private RectTransform nextTransform = null;
        [SerializeField] private RectTransform nextFramTransform = null;
        [SerializeField] private RectTransform nonSelectingTransform = null;

        public void Start()
        {
            GameDatas = GameDataService.Get();

            var icons = new List<Icon>(GameDatas.Count);

            var canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>() as Canvas;

            foreach (var gameData in GameDatas)
            {
                icons.Add(Icon.Instantiate(iconPrefab, canvas, gameData.Sprite, iconPrefab.Executable));
            }

            var iconList = new IconList(icons);

            IconsAnimBehavior = new IconsAnimBehavior_3Box(
                iconList,
                selectingTransform,
                selectingFrameTransform,
                prevTransform,
                prevFramTransform,
                nextTransform,
                nextFramTransform,
                nonSelectingTransform);
        }

        public void ExecuteSelecting()
        {
            IconsAnimBehavior.ExecuteSelecting();
        }

        public void ToSelectingNext()
        {
            IconsAnimBehavior.ToSelectingNext();
        }

        public void ToSelectingPrev()
        {
            IconsAnimBehavior.ToSelectingPrev();
        }

        private IIconsAnimBehavior IconsAnimBehavior { get; set; }
        List<GameData> GameDatas { get; set; }
    }
}
