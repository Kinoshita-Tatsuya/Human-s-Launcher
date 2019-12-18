using GameLauncher.Models.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace GameLauncher.Components
{
    [RequireComponent(typeof(Sprite), typeof(Animator))]
    public class Icon : MonoBehaviour
    {
        public static Icon Instantiate(Icon prefab, Canvas canvas, Sprite sprite, Executable executable)
        {
            var icon = Instantiate(prefab, canvas.transform, false) as Icon;

            var image = icon.GetComponent<Image>();
            image.sprite = sprite;
            icon.Executable = executable;

            return icon;
        }

        public Executable Executable { get; private set; }

        public void Execute()
        {
            Executable.Execute();
        }
    }
}
