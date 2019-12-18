using GameLauncher.Components.FlexibleAnimator;
using GameLauncher.Models.DomainObjects;
using GameLauncher.Models.RelateIcon;
using GameLauncher.Models.RelateIcon.IconsAnimBehavior;
using GameLauncher.Models.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Text TitleText = null;
        [SerializeField] private Text GenreText = null;
        [SerializeField] private Text SummaryText = null;

        public void Start()
        {
            GameDatas = GameDataService.Get();

            var icons = new List<Icon>(GameDatas.Count);

            var canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>() as Canvas;

            foreach (var gameData in GameDatas)
            {
                icons.Add(Icon.Instantiate(iconPrefab, canvas, gameData.Sprite, gameData.ExeAsExecutable));
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

            IconsAnimBehavior.OnSelectingChanged += UpdateText;
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

        private void UpdateText(int selectingIndex)
        {
            TitleText.text = GameDatas[selectingIndex]?.Title;
            GenreText.text = GameDatas[selectingIndex]?.Genre;
            SummaryText.text = GameDatas[selectingIndex]?.Summary;
        }

        private IIconsAnimBehavior IconsAnimBehavior { get; set; }
        List<GameData> GameDatas { get; set; }
    }
}
