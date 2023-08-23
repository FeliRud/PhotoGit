using System.Collections.Generic;
using System.Linq;

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

        public bool PuzzleAvailable(int id)
        {
            return Puzzles.FirstOrDefault(x => x.ID == id) != null;
        }

        public void LevelCompleted(int levelID)
        {
            if (levelID <= Level)
                return;
            
            Level++;
        }

        public int GetLevel()
        {
            return Level;
        }
    }
}