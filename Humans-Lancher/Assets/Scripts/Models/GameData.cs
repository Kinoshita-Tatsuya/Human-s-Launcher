using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumansLancher.Models
{
    public class GameData
    {
        public GameData(string title, string exePath, string texturePath,string descriptionPath)
        {
            Title = title;
            ExePath = exePath;
            TexturePath = texturePath;
            DescriptionPath = descriptionPath;
        }

        public string Title { get; private set; }
        public string ExePath { get; private set; }
        public string TexturePath { get; private set; }        
        public string DescriptionPath { get; set; }
    }
}
