using GameLauncher.Infrastructures;
using GameLauncher.Models.Commons;
using GameLauncher.Models.DomainObjects;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                var summary = FileErrorCatcher.CatchError(GetSummaryData, folderName + "/Summary.txt");
                var data = new GameData(
                    summary[0],
                    summary[1],
                    summary[2],
                    exeAsExecutable,
                    sprite,
                    descriptionAsExecutable);

                gameDatas.Add(data);
            }

            return gameDatas;
        }

        private static string[] GetSummaryData(string path)
        {
            string[] sammryData = new string[3];

            using (var file = new StreamReader(path, Encoding.UTF8))
            {
                string line = "";
                var sammaryText = new StringBuilder();

                sammryData[0] = file.ReadLine();
                sammryData[1] = file.ReadLine();

                while ((line = file.ReadLine()) != null)
                {
                    sammaryText.Append(line).Append("\n");
                }

                sammryData[2] = sammaryText.ToString();

                return sammryData;
            }
        }
    }    
}
