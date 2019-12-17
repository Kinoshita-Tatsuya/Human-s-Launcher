using System.Collections.Generic;
using UnityEngine;

namespace HumansLancher.UIs.IconList
{
    public class IconLocator_rail : MonoBehaviour
    {
        [SerializeField] Vector3 selectingScale = Vector3.zero;
        [SerializeField] Vector3 nonSelectingScale = Vector3.zero;
        [SerializeField] Transform prevGameIconTransform = null;
        [SerializeField] Transform nextGameIconTransform = null;
        [SerializeField] Vector3 posOutOfWindow = Vector3.zero;

        public void InitPos(List<Icon> icons)
        {
            var lastIndex = icons.Count - 1;

            icons[0].SetPos(transform.position);
            icons[0].SetScale(selectingScale);

            icons[1].SetPos(nextGameIconTransform.position);
            icons[1].SetScale(nonSelectingScale);

            icons[lastIndex].SetPos(prevGameIconTransform.position);
            icons[lastIndex].SetScale(nonSelectingScale);

            for (var i = 2; i < lastIndex; ++i)
            {
                icons[i].SetPos(posOutOfWindow);
            }
        }

        public void ToNext(List<Icon> icons)
        {
            if (icons.Count > 1)
            {
                translateVec = TRANSLATE_VEC.LEFT;

                AnimateIcons(icons);
            }
            else
            {

            }
        }

        public void ToPrev(List<Icon> icons)
        {
            if (icons.Count > 1)
            {
                translateVec = TRANSLATE_VEC.RIGHT;

                AnimateIcons(icons);
            }
            else
            {

            }
        }

        public void UpdatePos(List<Icon> icons)
        {
            DoCycleIconInList(icons);
        }

        public void AnimateIcons(List<Icon> icons)
        {
            icons[0].Animate("Scaling_L");

            for (var i = 1; i < icons.Count; ++i)
            {
                icons[i].Animate("Scaling_S");
            }
        }

        private enum TRANSLATE_VEC
        {
            NEUTRAL,
            RIGHT,
            LEFT,
        }

        void DoCycleIconInList(List<Icon> icons)
        {
            if (translateVec == TRANSLATE_VEC.LEFT)
            {
                List<Icon> iconsTmp = new List<Icon>(icons.ToArray());

                var lastIndex = icons.Count - 1;

                icons[0] = iconsTmp[lastIndex];

                for (var i = 1; i < icons.Count; ++i)
                {
                    icons[i] = iconsTmp[i - 1];
                }
            }
            else
            {
                List<Icon> iconsTmp = new List<Icon>(icons.ToArray());

                var lastIndex = icons.Count - 1;

                icons[lastIndex] = iconsTmp[0];

                for (var i = 0; i < icons.Count - 1; ++i)
                {
                    icons[i] = iconsTmp[i + 1];
                }
            }

            InitPos(icons);

            translateVec = TRANSLATE_VEC.NEUTRAL;
        }

        TRANSLATE_VEC translateVec = TRANSLATE_VEC.NEUTRAL;
    }
}
