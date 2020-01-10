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
            LapTime = Mathf.Max(LapTime - Time.deltaTime, 0.0f);
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

            if (GetAxis("Horizontal") > 0)
            {
                SelectingFrame.ToRight();
                LapTime = reinputTime_s;
            }

            if (GetAxis("Horizontal") < 0)
            {
                SelectingFrame.ToLeft();
                LapTime = reinputTime_s;
            }
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

        private float GetAxis(string str)
        {
            if (!CanInput) return 0;
            float axisValue = Input.GetAxis(str);
            return axisValue;
        }

        private bool CanAnimation { get; set; } = false;
        private bool ToggleCanAnimation()
        {
            return CanAnimation = !CanAnimation;
        }

        private GameDataPresenter GameDataPresenter { get; set; }
        private SelectingFrame SelectingFrame { get; set; }

        private float PrevHorizontal { get; set; }
        private readonly float reinputTime_s = 0.1f;
        private bool CanInput => LapTime <= 0;
        private float LapTime;

    }
}
