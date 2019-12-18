using System;

namespace GameLauncher.Models.RelateIcon.IconsAnimBehavior
{
    public interface IIconsAnimBehavior
    {
        event Action<int> OnSelectingChanged;

        IconList IconList { get; }
        bool CanExecute { get; }

        void ExecuteSelecting();

        void ToSelectingNext();

        void ToSelectingPrev();
    }
}
