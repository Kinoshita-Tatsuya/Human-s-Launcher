using GameLauncher.Infrastructures;
using GameLauncher.Models.Commons;
using GameLauncher.Models.DomainObjects;
using System.Collections.Generic;

namespace GameLauncher.Models.Services
{
    public static class GameDataService
    {
        public static List<GameData> Get()
        {
            var gameDatas = new List<GameData>();

            foreach (var folderName in GameListDAO.Get()) 
            {               
                var exeAsExecutable = new Executable(folderName + "/Exe.lnk");
                var sprite = SpriteFactory.Create(folderName + "/Icon.png");
                var descriptionAsExecutable = new Executable(folderName + "/Description.pdf");
                var summary = TextFileLoader.GetAllLine(folderName + "/Summary.txt");

                var data = new GameData(
                    "",
                    "",
                    summary,
                    exeAsExecutable,
                    sprite,
                    descriptionAsExecutable);

                gameDatas.Add(data);
            }

            return gameDatas;
        }        
    }    
}
