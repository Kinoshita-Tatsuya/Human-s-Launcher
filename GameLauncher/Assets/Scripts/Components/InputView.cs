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
            if (Input.GetKeyDown(KeyCode.RightArrow) || 
                Input.GetKeyDown(KeyCode.D))                
            {
                GameDataPresenter.ToSelectingNext();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || 
                Input.GetKeyDown(KeyCode.A))
            {
                GameDataPresenter.ToSelectingPrev();
            }

            if(Input.GetKeyDown(KeyCode.Return) || 
               Input.GetKeyDown("joystick button 1"))
            {
                GameDataPresenter.ExecuteSelecting();
            }
        }    
        
        private GameDataPresenter GameDataPresenter { get; set; }
    }
}
