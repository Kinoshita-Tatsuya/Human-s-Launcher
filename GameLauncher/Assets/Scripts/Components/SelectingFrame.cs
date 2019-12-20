using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingFrame : MonoBehaviour
{
    [SerializeField] Image GameIconFrame = null;
    [SerializeField] Image DiscriptionFrame = null;
    [SerializeField] Image HeartFrame = null;

    RectTransform Rect = null;
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

        switch (positionNum)
        {
            case Position.GameIcon:
                break;
            case Position.Discription:
                GameIconFrame.enabled = true;
                positionNum = Position.GameIcon;
                break;
            case Position.Heart:
                DiscriptionFrame.enabled = true;
                positionNum = Position.Discription;
                break;
        }
    } 


    public void ToLeft()
    {
        GameIconFrame.enabled = DiscriptionFrame.enabled= HeartFrame.enabled = false;
        switch (positionNum)
        {
            case Position.GameIcon:
                DiscriptionFrame.enabled = true;
                positionNum = Position.Discription;
                break;
            case Position.Discription:
                HeartFrame.enabled = true;
                positionNum = Position.Heart;
                break;
            case Position.Heart:
                break;
        }
    }

    private Position positionNum { get; set; }
    enum Position
    {
        GameIcon,
        Discription,
        Heart,
    }
}
