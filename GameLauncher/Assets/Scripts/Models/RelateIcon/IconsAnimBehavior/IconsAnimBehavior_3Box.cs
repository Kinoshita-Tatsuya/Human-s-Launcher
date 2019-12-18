using GameLauncher.Components.FlexibleAnimator;
using UnityEngine;

namespace GameLauncher.Models.RelateIcon.IconsAnimBehavior
{
    public class IconsAnimBehavior_3Box : IIconsAnimBehavior
    {
        public IconsAnimBehavior_3Box(
            IconList iconList,
            RectTransform selectingTransform,
            RectTransform selectingFrameTransform,
            RectTransform prevTransform,
            RectTransform prevFrameTransform,
            RectTransform nextTransform,
            RectTransform nextFrameTransform,
            RectTransform nonSelectingTransform)
        {
            IconList = iconList;
            SelectingTransform    = selectingTransform;
            SelectingFrameTransform = selectingFrameTransform;
            PrevTransform         = prevTransform;
            PrevFrameTransform = prevFrameTransform;
            NextTransform         = nextTransform;
            NextFrameTransform = nextFrameTransform;
            NonSelectingTransform = nonSelectingTransform;

            var iconFlexAnim = IconList.Selecting.GetComponent<IconFlexibleAnimator>();

            iconFlexAnim.OnAnimationStarted += () => CanExecute = false;
            iconFlexAnim.OnAnimationEnded   += () => CanExecute = true;
            iconFlexAnim.OnAnimationMiddlePassed += UpdateSelecting;

            InitPos();
        }

        public RectTransform SelectingTransform { get; private set; }
        public RectTransform SelectingFrameTransform { get; private set; }
        public RectTransform PrevTransform { get; private set; }
        public RectTransform PrevFrameTransform { get; private set; }
        public RectTransform NextTransform { get; private set; }
        public RectTransform NextFrameTransform { get; private set; }
        public RectTransform NonSelectingTransform { get; private set; }
        public bool CanExecute { get; private set; } = true;
        public IconList IconList { get; private set; }

        public void ExecuteSelecting()
        {
            if (!CanExecute) return;

            IconList.Selecting?.Execute();
        }

        public void ToSelectingNext()
        {
            if (!CanExecute) return;

            // 選ばれているインデックスのほうが次のインデックスより大きいまたは同じ場合
            // ループしているということになる
            if (IconList.SelectingIndex >= IconList.NextIndex)
            {
                return;
            }

            TranslateVec = TRANSLATE_VEC.LEFT;

            StartAnimation();
        }

        public void ToSelectingPrev()
        {
            if (!CanExecute) return;

            // 選ばれているインデックスのほうが前のインデックスより小さいまたは同じ場合
            // ループしているということになる
            if (IconList.SelectingIndex <= IconList.PrevIndex)
            {
                return;
            }

            TranslateVec = TRANSLATE_VEC.RIGHT;

            StartAnimation();
        }

        private enum TRANSLATE_VEC
        {
            NEUTRAL,
            RIGHT,
            LEFT
        }

        private void InitPos()
        {
            var icons = IconList.Icons;

            if (icons.Count <= 0) return;

            var lastIndex = icons.Count - 1;

            if (IconList.Selecting == null) return;
            var selectingRectTransform = IconList.Selecting.transform as RectTransform;
            selectingRectTransform.position = SelectingTransform.position;
            selectingRectTransform.sizeDelta = SelectingTransform.sizeDelta;

            var selectingFrameRectTransform = IconList.Selecting.transform.GetChild(0) as RectTransform;
            selectingFrameRectTransform.position = SelectingFrameTransform.position;
            selectingFrameRectTransform.sizeDelta = SelectingFrameTransform.sizeDelta;

            var nextIndex = IconList.SelectingIndex + 1;
            var existsNext = nextIndex <= lastIndex;

            if (existsNext)
            {
                var nextRectTransform = icons[nextIndex].transform as RectTransform;
                nextRectTransform.position = NextTransform.position;
                nextRectTransform.sizeDelta = NextTransform.sizeDelta;

                var frameRectTransform = icons[nextIndex].transform.GetChild(0) as RectTransform;
                frameRectTransform.position = NextFrameTransform.position;
                frameRectTransform.sizeDelta = NextFrameTransform.sizeDelta;
            }

            var prevIndex = IconList.SelectingIndex - 1;
            var existsPrev = prevIndex >= 0;

            if (existsPrev)
            {
                var prevRectTransform = icons[prevIndex].transform as RectTransform;
                prevRectTransform.position = PrevTransform.position;
                prevRectTransform.sizeDelta = PrevTransform.sizeDelta;

                var frameRectTransform = icons[prevIndex].transform.GetChild(0) as RectTransform;
                frameRectTransform.position = PrevFrameTransform.position;
                frameRectTransform.sizeDelta = PrevFrameTransform.sizeDelta;
            }

            for (var i = 0; i < icons.Count; ++i)
            {
                if (i == IconList.SelectingIndex ||
                    i == nextIndex               ||
                    i == prevIndex) continue;

                var rectTransform = icons[i].transform as RectTransform;
                rectTransform.position = NonSelectingTransform.position;
                rectTransform.sizeDelta = NonSelectingTransform.sizeDelta;

                var frameRectTransform = icons[i].transform.GetChild(0) as RectTransform;
                frameRectTransform.position = NonSelectingTransform.position;
                frameRectTransform.sizeDelta = NonSelectingTransform.sizeDelta;
            }
        }

        private void UpdateSelecting()
        {
            switch (TranslateVec)
            {
                case TRANSLATE_VEC.RIGHT:

                    IconList.ToPrev();
             
                    break;

                case TRANSLATE_VEC.LEFT:

                    IconList.ToNext();
                    
                    break;

                default:
                    break;
            }

            TranslateVec = TRANSLATE_VEC.NEUTRAL;

            InitPos();
        }

        private void StartAnimation()
        {
            foreach (var icon in IconList.Icons)
            {
                var flexAnim = icon.GetComponent<FlexibleAnimator>();

                flexAnim.Animator.SetTrigger("Scaling");
            }
        }

        TRANSLATE_VEC TranslateVec { get; set; }
    }
}
