using UnityEngine;

namespace GameLauncher.Components
{
    public class InputView : MonoBehaviour
    {
        private void Start()
        {
            GameDataPresenter = FindObjectOfType<GameDataPresenter>() as GameDataPresenter;
            SelectingFrame = FindObjectOfType<SelectingFrame>() as SelectingFrame;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                SwitchingExection();
            }
            if (Input.GetButtonDown("Heart"))
            {
                GameDataPresenter.IncreaseHeartNum();
            }
            if (Input.GetButtonDown("Discription"))
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

            if (Input.GetAxis("Horizontal") > 0 && GetHorizontalDown())
            {
                SelectingFrame.ToRight();
            }

            if (Input.GetAxis("Horizontal") < 0 && GetHorizontalDown())
            {
                SelectingFrame.ToLeft();
            }
            PrevHorizontal = Input.GetAxis("Horizontal");
        }

        bool GetHorizontalDown()
        {
            if (0 != PrevHorizontal) {
                return false;
            }
            return true;
        }

        private void SwitchingExection()
        {
            switch (SelectingFrame.PositionNum)
            {
                case SelectingFrame.Position.GameIcon:
                    GameDataPresenter.ExecuteSelecting();
                    break;
                case SelectingFrame.Position.Discription:
                    GameDataPresenter.ExecuteDiscription();
                    break;
                case SelectingFrame.Position.Heart:
                    GameDataPresenter.IncreaseHeartNum();
                    break;
            }
        }

        private bool CanAnimation { get; set; } = false;
        private bool ToggleCanAnimation()
        {
            return CanAnimation = !CanAnimation;
        }

        private GameDataPresenter GameDataPresenter { get; set; }
        private SelectingFrame SelectingFrame { get; set; }

        private float PrevHorizontal { get; set; }
    }
}
