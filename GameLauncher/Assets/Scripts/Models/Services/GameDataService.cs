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

            foreach (var path in GameDataDAO.Get())
            {
                var exeAsExecutable = new Executable(path["ExePath"].Get<string>());
                var sprite = SpriteFactory.Create(path["TexturePath"].Get<string>());
                var descriptionAsExecutable = new Executable(path["ExePath"].Get<string>());
                var summary = GameDataDAO.Get(path["SummaryPath"].Get<string>());

                var data = new GameData(
                    path["Title"].Get<string>(),
                    path["Genre"].Get<string>(),
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
