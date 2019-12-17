using HumansLancher.Models.Executable;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HumansLancher.UIs.IconList
{
    [RequireComponent(typeof(Sprite), typeof(Animator))]
    public class Icon : MonoBehaviour
    {
        public event Action<Icon> OnScalingDownStarted;
        public event Action<Icon> OnScalingDownEnded;
        public event Action<Icon> OnScalingUpCompleted;

        public void IgniteOnScalingDownStarted()
        {
            OnScalingDownStarted?.Invoke(this);
        }
        
        public void IgniteOnScalingDownEnded()
        {
            OnScalingDownEnded?.Invoke(this);
        }

        public void IgniteOnScalingUpCompleted()
        {
            OnScalingUpCompleted?.Invoke(this);
        }

        public static Icon Instantiate(Icon prefab, string name, Sprite sprite, AbstractExecutable executable)
        {
            var icon = Instantiate(prefab) as Icon;

            icon.Name = name;
            icon.Executable = executable;

            var renderer = icon.GetComponent<SpriteRenderer>();

            renderer.sprite = sprite;

            icon.animator = icon.GetComponent<Animator>();

            return icon;
        }

        public AbstractExecutable Executable { get; private set; }

        public string Name { get; private set; }

        public void Execute()
        {
            Executable.Execute();
        }

        public void SetPos(Vector3 pos)
        {
            transform.position = pos;            
        }

        public void Animate(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        Animator animator;
    }
}
