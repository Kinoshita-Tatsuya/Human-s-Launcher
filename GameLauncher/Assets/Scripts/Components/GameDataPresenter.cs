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
        [SerializeField] private Image TextBack = null;
        [SerializeField] private Text TitleText = null;
        [SerializeField] private Text GenreText = null;
        [SerializeField] private Text SummaryText = null;
        [SerializeField] private Text ToNextText = null;
        [SerializeField] private Text ToPrevText = null;

        public void Start()
        {
            GameDatas = GameDataService.Get();

            var icons = new List<Icon>(GameDatas.Count);

            var canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>() as Canvas;

            foreach (var gameData in GameDatas)
            {
                icons.Add(Icon.Instantiate(iconPrefab, canvas, gameData.Sprite, gameData.ExeAsExecutable));
            }

            IconList = new IconList(icons);
            IconsAnimBehavior = new IconsAnimBehavior_3Box(
                IconList,
                selectingTransform,
                selectingFrameTransform,
                prevTransform,
                prevFramTransform,
                nextTransform,
                nextFramTransform,
                nonSelectingTransform);

            IconsAnimBehavior.OnSelectingChanged += UpdateGameState;
            IconsAnimBehavior.OnSelectingChanged += (_) => { ToggleColorIconExistence(); };

            UpdateGameState(0);
            ToggleColorIconExistence();
        }

        public void ExecuteSelecting()
        {
            IconsAnimBehavior.ExecuteSelecting();
        }

        public void ExecuteDiscription()
        {
            var index = IconsAnimBehavior.IconList.SelectingIndex;
            GameDatas[index].DescriptionAsExecutable.Execute();
        }

        public void ToSelectingNext()
        {
            IconsAnimBehavior.ToSelectingNext();
        }

        public void ToSelectingPrev()
        {
            IconsAnimBehavior.ToSelectingPrev();
        }

        private void UpdateGameState(int index)
        {
            TitleText.text = GameDatas[index]?.Title;
            GenreText.text = GameDatas[index]?.Genre;
            SummaryText.text = GameDatas[index]?.Summary;
            TextBack.sprite = GameDatas[index]?.Sprite;
        }

        private void ToggleColorIconExistence()
        {
            ToNextText.color = ToPrevText.color = Color.white;

            var nextIndex = IconList.SelectingIndex + 1;
            var existsNext = nextIndex <= IconList.Icons.Count-1;

            if (!existsNext)
            {
                ToNextText.color = Color.gray;
            }

            var prevIndex = IconList.SelectingIndex - 1;
            var existsPrev = prevIndex >= 0;

            if (!existsPrev)
            {
                ToPrevText.color = Color.gray;
            }
        }

        private IconList IconList { get; set; }
        private IIconsAnimBehavior IconsAnimBehavior { get; set; }
        List<GameData> GameDatas { get; set; }
    }
}
