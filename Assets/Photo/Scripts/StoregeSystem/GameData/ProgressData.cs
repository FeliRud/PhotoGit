using System.Collections.Generic;

namespace Photo
{
    [System.Serializable]
    public class ProgressData
    {
        public int Level;
        public List<Puzzle> Puzzles;

        public ProgressData()
        {
            Reset();
        }

        public void Reset()
        {
            Puzzles = new List<Puzzle>();
            Level = 0;
        }

        public void AddPuzzle(int id)
        {
            Puzzles.Add(new Puzzle(id));
        }

        public List<Puzzle> GetPuzzles()
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