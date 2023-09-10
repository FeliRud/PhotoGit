using System.Collections.Generic;
using System.Linq;
using Photo.Scripts.Services;
using UnityEngine;

namespace Photo.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<int,LevelStaticData> _levels;

        public void LoadLevel() => 
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.PuzzleInfo.ID, x => x);

        public LevelStaticData ForLevel(int puzzleID) => 
            _levels.TryGetValue(puzzleID, out LevelStaticData staticData) 
                ? staticData 
                : null;

        public List<LevelStaticData> GetLevels() => 
            _levels.Values.ToList();
    }
}