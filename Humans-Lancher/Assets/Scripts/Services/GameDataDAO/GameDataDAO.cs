using System.Collections.Generic;
using HumansLancher.Models;
using HumansLancher.Services.Json;

namespace HumansLancher.Services.GameDataDAO
{
    public class GameDataDAO : IGameDataDAO
    {
        public IReadOnlyList<GameData> GetGameDatas()
        {
            List<GameData> gameDatas = new List<GameData>();

            foreach(var path in JsonReader.ReadJson(JsonFilePath)["FilePaths"])
            {
                var data = new GameData(            
                    path["Title"].Get<string>(),
                    path["ExePath"].Get<string>(),
                    path["TexturePath"].Get<string>(),
                    path["DescriptionPath"].Get<string>());

                gameDatas.Add(data);
            }

            return gameDatas;               
        }

        private string JsonFilePath { get; set; } = "Assets/Resources/FilePaths.json";
    }
}
