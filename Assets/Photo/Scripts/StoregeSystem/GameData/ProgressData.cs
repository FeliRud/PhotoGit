using System.Collections.Generic;
using System.Linq;

namespace Photo
{
    [System.Serializable]
    public class ProgressData
    {
        public bool Prehistory;
        public int Level;
        public List<PuzzleData> Puzzles;

        public ProgressData() => 
            Reset();

        public void Reset()
        {
            Prehistory = false;
            Puzzles = new List<PuzzleData>();
            Level = 0;
        }

        public void PrehistoryComplete() => 
            Prehistory = true;

        public void AddPuzzle(int id) => 
            Puzzles.Add(new PuzzleData(id));

        public bool PuzzleAvailable(int id) => 
            Puzzles.FirstOrDefault(x => x.ID == id) != null;

        public int GetLevel() => 
            Level;

        public void LevelCompleted(int levelID)
        {
            if (levelID <= Level)
                return;
            
            Level++;
        }
    }
}