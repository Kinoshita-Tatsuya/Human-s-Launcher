using UnityEngine;
using HumansLancher.UIs.IconList;

namespace HumansLancher.Models
{
    public class LancherManager : MonoBehaviour
    {
        [SerializeField] IconList iconList = null;

        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))                
            {
                iconList.ToNext();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                iconList.ToPrev();
            }

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKey("joystick button 1"))
            {
                iconList.SelectingExecutable?.Execute();
            }
        }           
    }
}
