using System;
using System.Collections.Generic;
using UnityEngine;

namespace HumansLancher.UIs.IconList
{
    public class IconLocator_circle : MonoBehaviour, IIconLocator
    {
        [SerializeField] float radius = 0.0f;
        [SerializeField] float rotationSpeed_s = 0.0f;

        public event Action OnMovingStarted;
        public event Action OnMovingEnded;

        public void InitPos(List<Icon> icons)
        {
            SetPos(icons, 0);
        }

        public void ToNext(List<Icon> icons)
        {
            if (icons.Count > 0)
            {
                OnMovingStarted?.Invoke();

                rotationVec = ROTATION_VEC.LEFT;
            }
            else
            {

            }
        }

        public void ToPrev(List<Icon> icons)
        {
            if (icons.Count > 0)
            {
                OnMovingStarted?.Invoke();

                rotationVec = ROTATION_VEC.RIGHT;
            }
            else
            {

            }
        }

        public void UpdatePos(List<Icon> icons)
        {
            if (rotationVec == ROTATION_VEC.NEUTRAL)
            {
                return;
            }

            var destRotationDelta =
                (rotationVec == ROTATION_VEC.LEFT ? 1.0f : -1.0f)
                * 360.0f / icons.Count;

            var additionRotation = destRotationDelta * rotationSpeed_s * Time.deltaTime;

            rotationDelta += additionRotation;

            if (rotationVec == ROTATION_VEC.LEFT)
            {
                rotationDelta = Mathf.Min(rotationDelta, destRotationDelta);

                if (rotationDelta >= destRotationDelta)
                {
                    List<Icon> iconsTmp = new List<Icon>(icons.ToArray());

                    var lastIndex = icons.Count - 1;

                    icons[0] = iconsTmp[lastIndex];

                    for (var i = 1; i < icons.Count; ++i)
                    {
                        icons[i] = iconsTmp[i - 1];
                    }

                    RunMovingEndingAction();
                }
            }

            if (rotationVec == ROTATION_VEC.RIGHT)
            {
                rotationDelta = Mathf.Max(rotationDelta, destRotationDelta);

                if (rotationDelta <= destRotationDelta)
                {
                    List<Icon> iconsTmp = new List<Icon>(icons.ToArray());

                    var lastIndex = icons.Count - 1;

                    icons[lastIndex] = iconsTmp[0];

                    for (var i = 0; i < icons.Count - 1; ++i)
                    {
                        icons[i] = iconsTmp[i + 1];
                    }

                    RunMovingEndingAction();
                }
            }

            SetPos(icons, rotationDelta);
        }

        private enum ROTATION_VEC
        {
            NEUTRAL,
            RIGHT,
            LEFT,
        }

        void SetPos(List<Icon> icons, float additionRotation)
        {
            for (var i = 0; i < icons.Count; ++i)
            {
                var radiusVec = radius * Vector3.back;

                var rotation = Quaternion.Euler(0.0f, i * (360.0f / icons.Count) + additionRotation, 0.0f);                

                icons[i].SetPos(rotation * radiusVec + transform.position);
            }
        }

        void RunMovingEndingAction()
        {
            rotationVec = ROTATION_VEC.NEUTRAL;

            rotationDelta = 0.0f;

            OnMovingEnded?.Invoke();
        }

        ROTATION_VEC rotationVec = ROTATION_VEC.NEUTRAL;
        float rotationDelta = 0.0f;
    }
}
