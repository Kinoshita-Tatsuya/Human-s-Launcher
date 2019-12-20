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
            if (Input.GetAxis("Heart") > 0)
            {
                //ハートボタン押下
            }
            if (Input.GetAxis("Discription") > 0)
            {
                //詳細表示
                GameDataPresenter.ExecuteDiscription();
            }

        }

        private void FixedUpdate()
        {
            ToggleCanAnimation();

            if (!CanAnimation) return;
            if (Input.GetAxis("Vertical") < 0)
            {
                GameDataPresenter.ToSelectingNext();
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                GameDataPresenter.ToSelectingPrev();
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                //GameDataPresenter.ToSelectingNext();
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                //GameDataPresenter.ToSelectingPrev();
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
