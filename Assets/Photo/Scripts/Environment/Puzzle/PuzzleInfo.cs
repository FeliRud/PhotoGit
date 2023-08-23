using UnityEngine;

namespace Photo
{
    [CreateAssetMenu(fileName = "Puzzle", menuName = "Equipment/PuzzleInfo", order = 51)]
    public class PuzzleInfo : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;

        public int ID => _id;
        public Sprite Sprite => _sprite;
    }
}