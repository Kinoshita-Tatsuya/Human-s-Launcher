using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameLauncher.Components
{
    public class HeartButton : MonoBehaviour
    {
        [SerializeField] private float reinputTime_s = 0.0f;

        public void IncreaseHeartNum(string key)
        {
            if (!CanInput) return;

            LapTime = reinputTime_s;
            var incrementHeartNum = GetCurrentHeartNum(key) + 1;
            PlayerPrefs.SetInt(key, incrementHeartNum);
            PlayerPrefs.Save();            
        }

        public int GetCurrentHeartNum(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        private void Update()
        {
            LapTime = Mathf.Max(LapTime - Time.deltaTime, 0.0f);
        }

        private bool CanInput => LapTime <= 0;
        private float LapTime;
    }
}
