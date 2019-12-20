﻿using GameLauncher.Components.FlexibleAnimator;
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
        [SerializeField] private HeartButton heartButton = null;
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
        [SerializeField] private Text HeartNumText = null;

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
            SelectingGameData?.DescriptionAsExecutable.Execute();
        }

        public void ToSelectingNext()
        {
            IconsAnimBehavior.ToSelectingNext();
        }

        public void ToSelectingPrev()
        {
            IconsAnimBehavior.ToSelectingPrev();
        }

        public void IncreaseHeartNum()
        {    
            heartButton.IncreaseHeartNum(SelectingGameData?.Title);
            UpdateHeartText();
        }

        private void UpdateGameState(int index)
        {
            TitleText.text = SelectingGameData?.Title;
            GenreText.text = SelectingGameData?.Genre;
            SummaryText.text = SelectingGameData?.Summary;
            TextBack.sprite = SelectingGameData?.Sprite;
            UpdateHeartText();
        }

        private void UpdateHeartText()
        {
            var heartNum = heartButton.GetCurrentHeartNum(SelectingGameData?.Title);
            HeartNumText.text = heartNum.ToString();
        }

        private void ToggleColorIconExistence()
        {
            ToNextText.color = ToPrevText.color = Color.white;

            var icons = IconList.Icons;

            var canLooping = icons.Count > 2;

            var nextIndex = canLooping ? IconList.NextIndex : IconList.SelectingIndex + 1;
            var existsNext = canLooping ? true : nextIndex <= IconList.Icons.Count - 1;

            if (!existsNext)
            {
                ToNextText.color = Color.gray;
            }

            var prevIndex = canLooping ? IconList.PrevIndex : IconList.SelectingIndex - 1;
            var existsPrev = canLooping ? true : prevIndex >= 0;

            if (!existsPrev)
            {
                ToPrevText.color = Color.gray;
            }
        }

        private GameData SelectingGameData => GameDatas[IconList.SelectingIndex];

        private IconList IconList { get; set; }
        private IIconsAnimBehavior IconsAnimBehavior { get; set; }
        List<GameData> GameDatas { get; set; }
    }
}
