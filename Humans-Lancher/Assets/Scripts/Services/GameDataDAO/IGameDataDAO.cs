using System.Collections.Generic;
using HumansLancher.Models;

namespace HumansLancher.Services.GameDataDAO
{
    public interface IGameDataDAO
    {
        IReadOnlyList<GameData> GetGameDatas();
    }

}
