using System.Collections.Generic;
using Photo.StaticData;

namespace Photo.Scripts.Services
{
    public interface IStaticDataService
    {
        void LoadLevel();
        LevelStaticData ForLevel(int puzzleID);
        List<LevelStaticData> GetLevels();
    }
}