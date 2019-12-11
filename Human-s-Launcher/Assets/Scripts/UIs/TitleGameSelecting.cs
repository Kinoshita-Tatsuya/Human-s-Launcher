using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HumansLancher.Models;

namespace HumansLancher.UIs
{
    public class TitleGameSelecting : MonoBehaviour
    {
        private Text titleText;

        // Start is called before the first frame update
        void Start()
        {
            titleText = gameObject.GetComponent<Text>();
            var lancher = GameObject.Find("LancherManager").GetComponent<LancherManager>();
            lancher.selectingNumChanged += new EventHandler((s, e) => SettingTitle());

            SettingTitle();
        }

        void SettingTitle()
        {            
            var lancher = GameObject.Find("LancherManager").GetComponent<LancherManager>();

            int selectingNum = lancher.SelectingNum;

            titleText.text = lancher.gameDatas[selectingNum].Title;
        }
    }
}
