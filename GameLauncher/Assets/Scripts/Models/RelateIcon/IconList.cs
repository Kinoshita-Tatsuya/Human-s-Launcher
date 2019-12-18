using GameLauncher.Components;
using GameLauncher.Components.FlexibleAnimator;
using System.Collections.Generic;
using UnityEngine;

namespace GameLauncher.Models.RelateIcon
{
    public class IconList
    {
        public IconList(IReadOnlyList<Icon> icons)
        {
            Icons = icons;

            if (Icons.Count <= 0) return;

            var iconFlexAnim = Icons[0].GetComponent<IconFlexibleAnimator>();
        }

        public int SelectingIndex { get; private set; }
        public int NextIndex => (SelectingIndex + 1) % Icons.Count;
        public int PrevIndex => SelectingIndex - 1 >= 0 ? SelectingIndex - 1 : Mathf.Max(Icons.Count - 1, 0);
        public Icon Selecting => Icons.Count > 0 ? Icons[SelectingIndex] : null;
        public Icon Next => Icons.Count > 0 ? Icons[NextIndex] : null;
        public Icon Prev => Icons.Count > 0 ? Icons[PrevIndex] : null;
        public IReadOnlyList<Icon> Icons { get; private set; }

        public void ToNext()
        {
            SelectingIndex = NextIndex;
        }

        public void ToPrev()
        {
            SelectingIndex = PrevIndex;
        }
    }
}
