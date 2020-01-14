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
            if (GameDataPresenter.IsPlayingAnimation) return;

            if (Input.GetButtonDown("Submit"))
            {
                GameDataPresenter.SwitchingExection();
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

            if (!CanAnimation) return;
            if (GameDataPresenter.IsPlayingAnimation) return;

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
                GameDataPresenter.ToSelectingFrameRight();
                LapTime = reinputTime_s;
            }

            if (GetAxis("Horizontal") < 0)
            {
                GameDataPresenter.ToSelectingFrameLeft();
                LapTime = reinputTime_s;
            }
        }        

        private float GetAxis(string str)
        {
            float axisValue = Input.GetAxis(str);
            return axisValue;
        }

        private bool CanAnimation => LapTime <= 0.0f;

        private GameDataPresenter GameDataPresenter { get; set; }        

        private float PrevHorizontal { get; set; }
        private readonly float reinputTime_s = 0.3f;
        private float LapTime;

    }
}
