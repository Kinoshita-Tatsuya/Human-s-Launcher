using UnityEngine;

namespace GameLauncher.Components
{
    public class InputView : MonoBehaviour
    {
        private void Start()
        {
            GameDataPresenter = FindObjectOfType<GameDataPresenter>() as GameDataPresenter;
        }

        private void Update()
        {
            if (Input.GetAxis("Submit") > 0)
            {
                GameDataPresenter.ExecuteSelecting();
            }
        }

        private void FixedUpdate()
        {
            ToggleCanAnimation();

            if (!CanAnimation) return;
            if (Input.GetAxis("Horizontal") > 0)
            {
                GameDataPresenter.ToSelectingNext();
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                GameDataPresenter.ToSelectingPrev();
            }
        }
        private bool CanAnimation { get; set; } = false;
        private bool ToggleCanAnimation()
        {
            return CanAnimation = !CanAnimation;
        }

        private GameDataPresenter GameDataPresenter { get; set; }
    }
}
