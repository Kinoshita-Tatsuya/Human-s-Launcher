using System;
using System.Collections.Generic;

namespace HumansLancher.UIs.IconList
{
    public interface IIconLocator
    {
        event Action OnMovingStarted;
        event Action OnMovingEnded;

        void InitPos(List<Icon> icons);

        void ToNext(List<Icon> icons);

        void ToPrev(List<Icon> icons);

        void UpdatePos(List<Icon> icons);
    }
}
