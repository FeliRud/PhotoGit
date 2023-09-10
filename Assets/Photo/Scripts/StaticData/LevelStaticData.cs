using UnityEngine;

namespace Photo.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        public int LevelID;
        public int SceneID;
        public PuzzleInfo PuzzleInfo;
        public GameObject PuzzlePrefab;
    }
}