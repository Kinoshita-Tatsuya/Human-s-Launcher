using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingFrame : MonoBehaviour
{
    [SerializeField] Image GameIcon = null;
    [SerializeField] Text Discription = null;
    [SerializeField] Text Heart = null;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameIcon.transform.position;
        this. = GameIcon.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
