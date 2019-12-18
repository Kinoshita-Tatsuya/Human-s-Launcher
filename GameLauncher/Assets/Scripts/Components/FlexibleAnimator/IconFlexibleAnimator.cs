using System;
using UnityEngine;

namespace GameLauncher.Components.FlexibleAnimator
{
    [RequireComponent(typeof(Animator))]
    public class IconFlexibleAnimator : FlexibleAnimator
    {
        public event Action OnAnimationStarted;
        public event Action OnAnimationMiddlePassed;
        public event Action OnAnimationEnded;

        public void IgniteOnAnimationStarted()
        {
            OnAnimationStarted?.Invoke();
        }

        public void IgniteOnAnimationMiddlePassed()
        {
            OnAnimationMiddlePassed?.Invoke();
        }

        public void IgniteOnAnimationEnded()
        {
            OnAnimationEnded?.Invoke();
        }
    }
}
