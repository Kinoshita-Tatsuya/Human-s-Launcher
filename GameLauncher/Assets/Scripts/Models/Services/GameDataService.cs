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
                var exeAsExecutable = new Executable(CreateExePath(folderName));
                var sprite = SpriteFactory.Create(CreateTexturePath(folderName));
                var descriptionAsExecutable = new Executable(CreateDescriptionPath(folderName));
                var summary = TextFileLoader.GetAllLine(CreateSummaryPath(folderName));

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

        private static string CreateExePath(string folderName)
        {
            return EXTERNAL_FILE_PATH + folderName + "/Exe.lnk";
        }

        private static string CreateTexturePath(string folderName)
        {
            return EXTERNAL_FILE_PATH + folderName + "/Icon.png";
        }

        private static string CreateDescriptionPath(string folderName)
        {
            return EXTERNAL_FILE_PATH + folderName + "/Description.pdf";
        }

        private static string CreateSummaryPath(string folderName)
        {
            return EXTERNAL_FILE_PATH + folderName + "/Summary.txt";
        }

        private const string EXTERNAL_FILE_PATH = "ExternalFiles/";
    }    
}
