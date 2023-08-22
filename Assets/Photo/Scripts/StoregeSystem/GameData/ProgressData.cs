using System.Collections.Generic;

namespace Photo
{
    [System.Serializable]
    public class ProgressData
    {
        public int Level;
        public List<PuzzleData> Puzzles;

        public ProgressData()
        {
            Reset();
        }

        public void Reset()
        {
            Puzzles = new List<PuzzleData>();
            Level = 0;
        }

        public void AddPuzzle(int id)
        {
            Puzzles.Add(new PuzzleData(id));
        }

        public List<PuzzleData> GetPuzzles()
        {
            return Puzzles;
        }

        public void LevelCompleted()
        {
            Level++;
        }

        public int GetLevel()
        {
            return Level;
        }
    }
}