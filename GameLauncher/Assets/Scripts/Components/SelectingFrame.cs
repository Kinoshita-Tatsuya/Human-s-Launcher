using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingFrame : MonoBehaviour
{
    [SerializeField] Image GameIconFrame = null;
    [SerializeField] Image DiscriptionFrame = null;
    [SerializeField] Image HeartFrame = null;

    // Start is called before the first frame update
    void Start()
    {
        DiscriptionFrame.enabled = HeartFrame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToRight()
    {
        GameIconFrame.enabled = DiscriptionFrame.enabled = HeartFrame.enabled = false;

        switch (PositionNum)
        {
            case Position.GameIcon:
                GameIconFrame.enabled = true;
                break;
            case Position.Discription:
                GameIconFrame.enabled = true;
                PositionNum = Position.GameIcon;
                break;
            case Position.Heart:
                DiscriptionFrame.enabled = true;
                PositionNum = Position.Discription;
                break;
        }
    } 


    public void ToLeft()
    {
        GameIconFrame.enabled = DiscriptionFrame.enabled= HeartFrame.enabled = false;
        switch (PositionNum)
        {
            case Position.GameIcon:
                DiscriptionFrame.enabled = true;
                PositionNum = Position.Discription;
                break;
            case Position.Discription:
                HeartFrame.enabled = true;
                PositionNum = Position.Heart;
                break;
            case Position.Heart:
                HeartFrame.enabled = true;
                break;
        }
    }

    public Position PositionNum { get; private set; }
    public enum Position
    {
        GameIcon,
        Discription,
        Heart,
    }
}
