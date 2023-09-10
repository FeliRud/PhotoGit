namespace Photo
{
    [System.Serializable]
    public class PuzzleData
    {
        public int ID;

        public PuzzleData(int id) => 
            ID = id;

        public int GetID() => 
            ID;
    }
}