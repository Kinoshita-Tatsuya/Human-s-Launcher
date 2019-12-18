namespace GameLauncher.Models.RelateIcon.IconsAnimBehavior
{
    public interface IIconsAnimBehavior
    {
        IconList IconList { get; }
        bool CanExecute { get; }

        void ExecuteSelecting();

        void ToSelectingNext();

        void ToSelectingPrev();
    }
}
