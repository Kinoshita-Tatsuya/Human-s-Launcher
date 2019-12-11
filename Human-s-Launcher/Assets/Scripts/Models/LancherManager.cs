using System.IO;
using UnityEngine;
using MiniJSON;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace HumansLancher.Models
{
    public class LancherManager : MonoBehaviour
    {
        public List<GameData> gameDatas { get; set; } = new List<GameData>();

        public int SelectingNum { get; private set; } = 0;

        public event EventHandler selectingNumChanged;

        private Process proc = new Process();

        // Start is called before the first frame update
        void Awake()
        {
            LoadFilePaths();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                IncrementSelectingNum();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                DecrementSelectingNum();
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                LaunchProcess();
            }
        }

        private void LoadFilePaths()
        {
            string jsonText = File.ReadAllText("Assets/Resources/FilePaths.json");

            JsonNode json = JsonNode.Parse(jsonText);

            foreach (var path in json["FilePaths"])
            {
                var data = new GameData();
                data.Title = path["Title"].Get<string>();
                data.ExePath = path["ExePath"].Get<string>();
                data.IconPath = path["IconPath"].Get<string>();
                data.DescriptionPath = path["DescriptionPath"].Get<string>();

                gameDatas.Add(data);
            }
        }

        private void IncrementSelectingNum()
        {
            SelectingNum++;
            SelectingNum = (SelectingNum > gameDatas.Count - 1) ? 0 : SelectingNum;
            selectingNumChanged(this,EventArgs.Empty);
        }

        private void DecrementSelectingNum()
        {
            SelectingNum--;
            SelectingNum = (SelectingNum < 0) ? gameDatas.Count - 1 : SelectingNum;
            selectingNumChanged(this, EventArgs.Empty);
        }
        
        private void LaunchProcess()
        {           
            proc.StartInfo.FileName = gameDatas[SelectingNum].ExePath;
            proc.Start();
        }

        private void OnApplicationQuit()
        {
            if (!proc.HasExited)
            {
                // 別アプリが起動中の場合のみ終了させる
                proc.CloseMainWindow();
            }

            proc.Close();
            proc = null;
        }
    }
}
